using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.XMPP.Payloads;
using FortniteDotNet.Models.XMPP.EventArgs;
using FortniteDotNet.Services;
using Newtonsoft.Json.Linq;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task HandleMessage(XmlDocument document)
        {
            switch (document.DocumentElement.GetAttribute("type"))
            {
                case "chat":
                    HandleChat(document);
                    return;
                case "error":
                case "headline":
                case "groupchat":
                    return;
            }

            var body = JsonConvert.DeserializeObject<dynamic>(document.DocumentElement.InnerText);
            Console.WriteLine((string)body.type);
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

                    var invite = ping.Invites.FirstOrDefault(x => x.SentBy == (string)body.pinger_id && x.Status == "SENT");
                    onPartyInvite(invite);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_JOINED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        break;
                    
                    var accountId = (string)body.account_id;

                    if (accountId == AuthSession.AccountId)
                    {
                        if (CurrentParty.Members.All(x => x.Id != accountId))
                        {
                            var member = new PartyMember
                            {
                                Id = accountId,
                                JoinedAt = DateTime.Now,
                                Revision = 0,
                                UpdatedAt = DateTime.Now,
                                Role = "Member",
                                Meta = PartyMember.SchemaMeta
                            };
                            
                            CurrentParty.Members.Add(member);
                            member.UpdateMember(member.Revision);
                            await PartyService.UpdateMember(AuthSession, member, CurrentParty.Id);
                        }
                        await SendPresence(new Presence
                        {
                            Status = $"Battle Royale Lobby - {CurrentParty.Members.Count} / {CurrentParty.Config["max_size"]}",
                            IsPlaying = true,
                            IsJoinable = false,
                            HasVoiceSupport = false,
                            SessionId = "",
                            Properties = new()
                            {
                                {
                                    "party.joininfodata.286331153_j", new Dictionary<string, object>
                                    {
                                        { "sourceId", AuthSession.AccountId },
                                        { "sourceDisplayName", AuthSession.DisplayName },
                                        { "sourcePlatform", "WIN" },
                                        { "partyId", CurrentParty.Id },
                                        { "partyTypeId", 286331153 },
                                        { "key", "k" },
                                        { "appId", "Fortnite" },
                                        { "buildId", "1:3:"},
                                        { "partyFlags", -2024557306 },
                                        { "notAcceptingReason", 0 },
                                        { "pc", CurrentParty.Members.Count }
                                    }
                                },
                                { 
                                    "FortBasicInfo_j", new Dictionary<string, object>
                                    {
                                        { "homeBaseRating", 1 }
                                    }
                                },
                                { "FortLFG_I", 0 },
                                { "FortPartySize_i", 1 },
                                { "FortSubGame_i", 1 },
                                { "InUnjoinableMatch_b", false },
                                {
                                    "FortGameplayStats_j", new Dictionary<string, object>
                                    {
                                        { "state", "" },
                                        { "playlist", "None" },
                                        { "numKills", 0 },
                                        { "bFellToDeath", false }
                                    }
                                },
                                /*{
                                    "KairosProfile_j", new Dictionary<string, object>
                                    {
                                        { "appInstalled", "init" },
                                        { "avatar", "CID_028_Athena_Commando_F" },
                                        { "avatarBackground", "[\"#8EFDE5\", \"#1CBA9E\", \"#034D3F\"]" }
                                    }
                                }*/
                            }                
                        });
                    }
                    else
                    {
                        CurrentParty.Members.Add(new PartyMember
                        {
                            Id = accountId,
                            JoinedAt = DateTime.Now,
                            Revision = 0,
                            UpdatedAt = DateTime.Now,
                            Role = "Member",
                            Meta = PartyMember.SchemaMeta
                        });
                    }

                    var partyMember = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    onPartyMemberJoined(partyMember);
                    
                    /*if (CurrentParty.Members.FirstOrDefault(x => x.Id == accountId).Role == "Leader")
                        await PartyService.UpdateParty(AuthSession, CurrentParty, new()
                        {
                            { "Default:Default:RawSquadAssignments_j", "" }
                        });*/
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_STATE_UPDATED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        break;
                    
                    var accountId = (string)body.account_id;
                    
                    var partyMember = CurrentParty.Members.FirstOrDefault(x => x.Id == accountId);
                    if (partyMember.Equals(default(PartyMember)))
                        break;

                    var revision = (int)body.revision;
                    var updated = ((JObject)body.member_state_updated).ToObject<Dictionary<string, object>>();
                    var deleted = ((JArray)body.member_state_removed).ToObject<List<string>>();
                    
                    partyMember.UpdateMember(revision, updated, deleted);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.PARTY_UPDATED":
                {
                    if (CurrentParty == null || CurrentParty.Id != (string)body.party_id)
                        break;
                    
                    var revision = (int)body.revision;
                    var config = ((JObject)body).ToObject<Dictionary<string, object>>();
                    var updated = ((JObject)body.party_state_updated).ToObject<Dictionary<string, object>>();
                    var deleted = ((JArray)body.party_state_removed).ToObject<List<string>>();
                    
                    CurrentParty.UpdateParty(revision, config, updated, deleted);
                    
                    await SendPresence(new Presence
                    {
                        Status = $"Battle Royale Lobby - {CurrentParty.Members.Count} / {CurrentParty.Config["max_size"]}",
                        IsPlaying = true,
                        IsJoinable = false,
                        HasVoiceSupport = false,
                        SessionId = "",
                        Properties = new()
                        {
                            {
                                "party.joininfodata.286331153_j", new Dictionary<string, object>
                                {
                                    { "sourceId", AuthSession.AccountId },
                                    { "sourceDisplayName", AuthSession.DisplayName },
                                    { "sourcePlatform", "WIN" },
                                    { "partyId", CurrentParty.Id },
                                    { "partyTypeId", 286331153 },
                                    { "key", "k" },
                                    { "appId", "Fortnite" },
                                    { "buildId", "1:3:"},
                                    { "partyFlags", -2024557306 },
                                    { "notAcceptingReason", 0 },
                                    { "pc", CurrentParty.Members.Count }
                                }
                            },
                            { 
                                "FortBasicInfo_j", new Dictionary<string, object>
                                {
                                    { "homeBaseRating", 1 }
                                }
                            },
                            { "FortLFG_I", 0 },
                            { "FortPartySize_i", 1 },
                            { "FortSubGame_i", 1 },
                            { "InUnjoinableMatch_b", false },
                            {
                                "FortGameplayStats_j", new Dictionary<string, object>
                                {
                                    { "state", "" },
                                    { "playlist", "None" },
                                    { "numKills", 0 },
                                    { "bFellToDeath", false }
                                }
                            },
                            /*{
                                "KairosProfile_j", new Dictionary<string, object>
                                {
                                    { "appInstalled", "init" },
                                    { "avatar", "CID_028_Athena_Commando_F" },
                                    { "avatarBackground", "[\"#8EFDE5\", \"#1CBA9E\", \"#034D3F\"]" }
                                }
                            }*/
                        }                
                    });
                    
                    break;
                }
                case "com.epicgames.social.party.notification.v0.MEMBER_LEFT":
                case "com.epicgames.social.party.notification.v0.MEMBER_EXPIRED":
                case "com.epicgames.social.party.notification.v0.MEMBER_KICKED":
                case "com.epicgames.social.party.notification.v0.MEMBER_DISCONNECTED":
                case "com.epicgames.social.party.notification.v0.MEMBER_NEW_CAPTAIN":
                case "com.epicgames.social.party.notification.v0.MEMBER_REQUIRE_CONFIRMATION":
                case "com.epicgames.social.party.notification.v0.INVITE_DECLINED":
                default:
                    //Console.WriteLine(body);
                    break;
            }
        }

        public void HandleChat(XmlDocument document)
        {
            onChatReceived(new ChatEventArgs
            {
                From = document.DocumentElement.GetAttribute("from"),
                Body = document.DocumentElement.InnerText
            });
        }
    }
}