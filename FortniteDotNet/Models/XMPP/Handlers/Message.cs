using System;
using System.Xml;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.XMPP.Payloads;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task HandleMessage(XmlDocument document)
        {
            switch (document.DocumentElement.GetAttribute("type"))
            {
                case "chat":
                    onChatReceived(new(document.DocumentElement));
                    return;
                case "groupchat":
                {
                    if (document.DocumentElement.InnerText == "Welcome! You created new Multi User Chat Room.")
                        return;
                    
                    var from = document.DocumentElement.GetAttribute("from");
                    if (CurrentParty == null || from.Split("@")[0].Replace("Party-", "") != CurrentParty.Id)
                        return;
            
                    var id = document.DocumentElement.GetAttribute("from").Split("/")[1].Split(":")[1];
                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == id);
                    if (member == null)
                        return;
                    
                    onGroupChatReceived(new(CurrentParty, document.DocumentElement));
                    return;
                }
                case "error":
                case "headline":
                    return;
            }

            var body = JsonConvert.DeserializeObject<dynamic>(document.DocumentElement.InnerText);
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
                    var payload = body.payload as FriendRemoval;
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
                    var pings = await PartyService.GetPartyPings(AuthSession, (string)body.pinger_id);
                    var ping = pings.FirstOrDefault();

                    if (ping == null)
                        throw new InvalidOperationException($"No invite from {(string)body.pinger_id} found!");
                    if (KickedPartyIds.Contains(ping.Id))
                        throw new InvalidOperationException($"Previously kicked from party {ping.Id}!");
                    
                    var invite = ping.Invites.FirstOrDefault(x => x.SentBy == (string)body.pinger_id && x.Status == "SENT") ?? new PartyInvite
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
                case "com.epicgames.social.party.notification.v0.MEMBER_JOINED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        return;
                    
                    var accountId = (string)body.account_id;
                    PartyMember member;
                    
                    if (accountId == AuthSession.AccountId)
                    {
                        member = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                        if (member == null)
                        {
                            var connection = ((JObject)body.connection).ToObject<PartyMemberConnection>();
                            member = new PartyMember
                            {
                                Id = (string)body.account_id,
                                Role = "",
                                JoinedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Revision = 0,
                                Meta = connection.Meta
                            };
                            
                            CurrentParty.Members.Add(member);
                        }

                        await PartyService.UpdateMember(AuthSession, member, CurrentParty.Id);
                    }
                    else
                    {
                        var connection = ((JObject)body.connection).ToObject<PartyMemberConnection>();
                        member = new PartyMember
                        {
                            Id = (string)body.account_id,
                            Role = "",
                            JoinedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Revision = 0,
                            Meta = connection.Meta
                        };
                        
                        CurrentParty.Members.Add(member);
                    }

                    await CurrentParty.UpdatePresence(this);
                    
                    var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                    if (me.IsLeader)
                        await CurrentParty.UpdateSquadAssignments(AuthSession);
                    
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

                    CurrentParty.UpdateParty(revision, config, updated, deleted);
                    await CurrentParty.UpdatePresence(this);
                    
                    onPartyUpdated(new(CurrentParty, updated, deleted));
                    
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
                    
                    member.UpdateMember(revision, updated, deleted);
                    onPartyMemberUpdated(new(member, updated, deleted));
                    
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
                    if (me.IsLeader)
                        await CurrentParty.UpdateSquadAssignments(AuthSession);
                    
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
                    if (me.IsLeader)
                        await CurrentParty.UpdateSquadAssignments(AuthSession);
                    
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
                        await PartyService.InitParty(this);
                    }
                    else
                    {
                        if (member == null)
                            return;
                        
                        CurrentParty.Members.Remove(member);
                        await CurrentParty.UpdatePresence(this);

                        var me = CurrentParty.Members.FirstOrDefault(x => x.Id == AuthSession.AccountId);
                        if (me.IsLeader)
                            await CurrentParty.UpdateSquadAssignments(AuthSession);
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
                    if (me.IsLeader)
                        await CurrentParty.UpdateSquadAssignments(AuthSession);
                    
                    onPartyMemberDisconnected(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_NEW_CAPTAIN":
                {
                    if (CurrentParty?.Leader == null || CurrentParty.Id != (string)body.party_id)
                        return;

                    CurrentParty.Leader.Role = "";
                    
                    var member = CurrentParty.Members.FirstOrDefault(x => x.Id == (string) body.account_id);
                    if (member == null)
                        return;

                    member.Role = "CAPTAIN";
                    await CurrentParty.UpdatePresence(this);
                    
                    onPartyMemberPromoted(member);
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_REQUIRE_CONFIRMATION":
                {
                    if (CurrentParty?.Leader == null || CurrentParty.Id != (string)body.party_id)
                        return;

                    if (CurrentParty.Leader.Id == AuthSession.AccountId)
                        await PartyService.ConfirmMember(AuthSession, CurrentParty.Id, (string)body.account_id);
                        
                    break;
                }
                case "com.epicgames.social.party.notification.v0.INVITE_DECLINED":
                {
                    var inviteeId = (string)body.invitee_id;
                    
                    var friend = await FriendsService.GetFriend(AuthSession, inviteeId);
                    if (friend != null)
                        onPartyInviteDeclined(friend);

                    break;
                }
                default:
                    Console.WriteLine($"Unknown XMPP message received! Type: {(string)body.type}");
                    break;
            }
        }
    }
}