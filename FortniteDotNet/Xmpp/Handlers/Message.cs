using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using FortniteDotNet.Enums.PartyService;
using FortniteDotNet.Models.PartyService;
using FortniteDotNet.Xmpp.EventArgs;
using FortniteDotNet.Xmpp.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        private async Task HandleMessage(XmlElement docElement)
        {
            switch (docElement.GetAttribute("type"))
            {
                case "chat":
                    onChatReceived(new ChatEventArgs(docElement));
                    return;
                case "groupchat":
                {
                    if (docElement.InnerText == "Welcome! You created new Multi User Chat Room.")
                        return;
                    
                    var from = docElement.GetAttribute("from");
                    if (CurrentParty == null || from.Split("@")[0].Replace("Party-", "") != CurrentParty.Id)
                        return;
            
                    var id = docElement.GetAttribute("from").Split("/")[1].Split(":")[1];
                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == id);
                    if (member == null)
                        return;
                    
                    onGroupChatReceived(new GroupChatEventArgs(CurrentParty, docElement));
                    return;
                }
                case "error":
                case "headline":
                    return;
            }

            var body = JsonConvert.DeserializeObject<dynamic>(docElement.InnerText);
            switch ((string)body.type)
            {
                case "com.epicgames.friends.core.apiobjects.Friend":
                {
                    var payload = JsonConvert.DeserializeObject<Friend>((string)body.payload.ToString());
                    if (payload.Status == "ACCEPTED")
                        onFriendRequestAccepted(payload);
                    else
                    {
                        if (payload.Direction == "INBOUND")
                            onFriendRequestReceived(payload);
                        else 
                            onFriendRequestSent(payload);
                    }
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.FriendRemoval":
                {
                    var payload = JsonConvert.DeserializeObject<FriendRemoval>((string)body.payload.ToString());
                    onFriendshipRemoved(payload);
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.BlockListEntryAdded":
                {
                    var payload = JsonConvert.DeserializeObject<BlockListEntry>((string)body.payload.ToString());
                    onUserBlocked(payload);
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.BlockListEntryRemoved":
                {
                    var payload = JsonConvert.DeserializeObject<BlockListEntry>((string)body.payload.ToString());
                    onUserUnblocked(payload);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.PING":
                {
                    var pings = await _partyService.GetPartiesByPinger(AuthSession, (string)body.pinger_id);
                    var ping = pings.FirstOrDefault();

                    if (ping == null)
                        throw new InvalidOperationException($"No invite from {(string)body.pinger_id} found!");
                    if (KickedPartyIds.Contains(ping.Id))
                        throw new InvalidOperationException($"Previously kicked from party {ping.Id}!");
                    
                    var invite = ping.Invites.FirstOrDefault(x => x.SentBy == (string)body.pinger_id && x.Status == "SENT") ?? new Invite
                    {
                        PartyId = ping.Id,
                        SentBy = (string)body.pinger_id,
                        Meta = ping.Meta,
                        SentTo = AuthSession.AccountId,
                        SentAt = (DateTime)body.sent,
                        UpdatedAt = (DateTime)body.sent,
                        ExpiresAt = (DateTime)body.expires,
                        Status = "SENT"
                    };

                    onPartyInviteReceived(invite);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.INITIAL_INVITE":
                {
                    var summary = await _partyService.GetSummary(AuthSession);
                    var invite = summary.Invites.OrderByDescending(x => x.SentAt).FirstOrDefault(x => x.SentBy == (string)body.inviter_id);

                    if (invite == null)
                        throw new InvalidOperationException($"No invite from {(string)body.inviter_id} found!");
                    if (KickedPartyIds.Contains(invite.PartyId))
                        throw new InvalidOperationException($"Previously kicked from party {invite.PartyId}!");

                    onPartyInviteReceived(invite);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_JOINED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    Member member;
                    
                    if (accountId == AuthSession.AccountId)
                    {
                        member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                        if (member == null)
                        {
                            var connection = ((JObject)body.connection).ToObject<Connection>();
                            member = new Member
                            {
                                Id = (string)body.account_id,
                                Role = Role.MEMBER,
                                JoinedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Revision = 0,
                                Meta = connection.Meta
                            };
                            
                            CurrentParty.Members.Add(member);
                        }

                        await JoinPartyChat();
                        await _partyService.UpdateMember(AuthSession, CurrentParty.Id, member.Id, new MemberUpdate(member));
                    }
                    else
                    {
                        var connection = ((JObject)body.connection).ToObject<Connection>();
                        member = new Member
                        {
                            Id = (string)body.account_id,
                            Role = Role.MEMBER,
                            JoinedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Revision = 0,
                            Meta = connection.Meta
                        };
                        
                        CurrentParty.Members.Add(member);
                    }

                    await CurrentParty.UpdatePresence(this);
                    
                    var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                    if (me.IsCaptain)
                        await CurrentParty.UpdateSquadAssignments(_partyService, AuthSession);
                    
                    onPartyMemberJoined(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.PARTY_UPDATED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var config = ((JObject)body).ToObject<Dictionary<string, object>>();
                    var deleted = ((JArray)body.party_state_removed).ToObject<List<string>>();
                    var updated = ((JObject)body.party_state_updated).ToObject<Dictionary<string, object>>();
                    var revision = (int)body.revision;

                    CurrentParty.Update(revision, config, updated, deleted);
                    await CurrentParty.UpdatePresence(this);
                    
                    onPartyUpdated(new PartyUpdatedEventArgs(CurrentParty, updated, deleted));
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_STATE_UPDATED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    
                    var deleted = ((JArray)body.member_state_removed).ToObject<List<string>>();
                    var updated = ((JObject)body.member_state_updated).ToObject<Dictionary<string, object>>();
                    var revision = (int)body.revision;

                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    if (member == null)
                        return;                  
                    
                    member.Update(revision, updated, deleted);
                    onPartyMemberUpdated(new PartyMemberUpdatedEventArgs(member, updated, deleted));
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_LEFT":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    if (accountId == AuthSession.AccountId)
                        return;

                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    if (member == null)
                        return;

                    CurrentParty.Members.Remove(member);
                    await CurrentParty.UpdatePresence(this);

                    var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                    if (me.IsCaptain)
                        await CurrentParty.UpdateSquadAssignments(_partyService, AuthSession);
                    
                    onPartyMemberLeft(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_EXPIRED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    if (accountId == AuthSession.AccountId)
                        return;

                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    if (member == null)
                        return;

                    CurrentParty.Members.Remove(member);
                    await CurrentParty.UpdatePresence(this);

                    var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                    if (me.IsCaptain)
                        await CurrentParty.UpdateSquadAssignments(_partyService, AuthSession);
                    
                    onPartyMemberExpired(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_KICKED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;

                    var accountId = (string) body.account_id;
                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    
                    if (accountId == AuthSession.AccountId)
                    {
                        KickedPartyIds.Add((string)body.party_id);
                        CurrentParty = null;
                        await InitParty();
                    }
                    else
                    {
                        if (member == null)
                            return;
                        
                        CurrentParty.Members.Remove(member);
                        await CurrentParty.UpdatePresence(this);

                        var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                        if (me.IsCaptain)
                            await CurrentParty.UpdateSquadAssignments(_partyService, AuthSession);
                    }
                    onPartyMemberKicked(member);

                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_DISCONNECTED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    if (accountId == AuthSession.AccountId)
                        return;

                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    if (member == null)
                        return;

                    CurrentParty.Members.Remove(member);
                    await CurrentParty.UpdatePresence(this);

                    var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                    if (me.IsCaptain)
                        await CurrentParty.UpdateSquadAssignments(_partyService, AuthSession);
                    
                    onPartyMemberDisconnected(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_NEW_CAPTAIN":
                {
                    if (CurrentParty?.Captain == null || CurrentParty.Id != (string)body.party_id)
                        return;

                    CurrentParty.Captain.Role = Role.MEMBER;
                    
                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == (string) body.account_id);
                    if (member == null)
                        return;

                    member.Role = Role.CAPTAIN;
                    await CurrentParty.UpdatePresence(this);
                    
                    onPartyMemberPromoted(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_REQUIRE_CONFIRMATION":
                {
                    if (CurrentParty?.Captain == null || CurrentParty.Id != (string)body.party_id)
                        return;

                    if (CurrentParty.Captain.Id == AuthSession.AccountId)
                        await _partyService.ConfirmMember(AuthSession, CurrentParty.Id, (string)body.account_id);
                        
                    break;
                }
                case "com.epicgames.social.party.notification.v0.INVITE_DECLINED":
                {
                    var inviteeId = (string)body.invitee_id;
                    
                    var friend = await _friendsService.GetFriend(AuthSession, inviteeId);
                    if (friend != null)
                        onPartyInviteDeclined(friend);

                    break;
                }
                default:
                    Console.WriteLine($"Unknown message received! Type: {(string)body.type}");
                    break;
            }
        }
    }
}