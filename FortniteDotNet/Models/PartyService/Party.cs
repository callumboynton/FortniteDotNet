using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FortniteDotNet.Enums.PartyService;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Services;
using FortniteDotNet.Xmpp;
using FortniteDotNet.Xmpp.Meta;
using FortniteDotNet.Xmpp.Payloads;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class Party
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("members")]
        public List<Member> Members { get; set; }

        [JsonProperty("applicants")]
        public List<Member> Applicants { get; set; }

        [JsonProperty("invites")]
        public List<Invite> Invites { get; set; }

        [JsonProperty("intentions")]
        public List<object> Intentions { get; set; }
        
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonIgnore] 
        public Member Captain => Members.FirstOrDefault(x => x.Role == Role.CAPTAIN);
        
        internal void Update(int revision, Dictionary<string, object> config, Dictionary<string, object> updated = null, IEnumerable<string> deleted = null)
        {
            if (revision > Revision)
                Revision = revision;
            
            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);
            foreach (var updatedMeta in updated)
                Meta[updatedMeta.Key] = updatedMeta.Value.ToString();
            
            Config["joinability"] = config["party_privacy_type"];
            Config["maxSize"] = config["max_number_of_members"];
            Config["subType"] = config["party_sub_type"];
            Config["type"] = config["party_type"];
            Config["inviteTtl"] = config["invite_ttl_seconds"];

            UpdatedAt = DateTime.UtcNow;
        }
        
        public async Task UpdatePrivacy(IPartyService partyService, AuthSession authSession, PartyPrivacy partyPrivacy)
        {
            var updated = new Dictionary<string, object>();
            var deleted = new List<string>();

            if (Meta.ContainsKey("Default:PrivacySettings_j"))
            {
                updated["Default:PrivacySettings_j"] = Meta["Default:PrivacySettings_j"] = JsonConvert.SerializeObject(new
                {
                    PrivacySettings = new PrivacySettings
                    {
                        PartyType = partyPrivacy.PartyType,
                        OnlyLeaderFriendsCanJoin = partyPrivacy.OnlyLeaderFriendsCanJoin,
                        PartyInviteRestriction = partyPrivacy.InviteRestriction
                    }
                });
            }

            updated["urn:epic:cfg:presence-perm_s"] = Meta["urn:epic:cfg:presence-perm_s"] = partyPrivacy.PresencePermission;
            updated["urn:epic:cfg:accepting-members_b"] = Meta["urn:epic:cfg:accepting-members_b"] = partyPrivacy.AcceptingMembers.ToString();
            updated["urn:epic:cfg:invite-perm_s"] = Meta["urn:epic:cfg:invite-perm_s"] = partyPrivacy.InvitePermission;

            if (partyPrivacy.PartyType == "Private")
            {
                deleted.Add("urn:epic:cfg:not-accepting-members");
                updated["urn:epic:cfg:not-accepting-members-reason_i"] = Meta["urn:epic:cfg:not-accepting-members-reason_i"] = "7";
                Config["discoverability"] = "INVITED_ONLY";
                Config["joinability"] = "INVITE_AND_FORMER";
            }
            else
            {
                deleted.Add("urn:epic:cfg:not-accepting-members-reason_i");
                Config["discoverability"] = "ALL";
                Config["joinability"] = "OPEN";
            }

            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);

            await partyService.UpdateParty(authSession, Id, new PartyUpdate(this, updated, deleted));
        }
        
        public async Task SetCustomMatchmakingKey(IPartyService partyService, AuthSession authSession, string key = null)
        {
            Meta["Default:CustomMatchKey_s"] = key ?? "";
            await partyService.UpdateParty(authSession, Id, new PartyUpdate(this, new Dictionary<string, object>
            {
                {"Default:CustomMatchKey_s", Meta["Default:CustomMatchKey_s"]}
            }));
        }
        
        public async Task SetPlaylist(IPartyService partyService, AuthSession authSession, string playlistId)
        {
            Meta["Default:PlaylistData_j"] = new PlaylistDataMeta(playlistId).ToString();
            await partyService.UpdateParty(authSession, Id, new PartyUpdate(this, new Dictionary<string, object>
            {
                {"Default:PlaylistData_j", Meta["Default:PlaylistData_j"]}
            }));
        }
        
        internal async Task UpdateSquadAssignments(IPartyService partyService, AuthSession authSession)
        {
            var assignments = new List<RawSquadAssignment>();
            var index = 0;
            
            assignments.Add(new RawSquadAssignment(authSession.AccountId));

            foreach (var member in Members)
            {
                if (member.Id == authSession.AccountId) 
                    continue;
                
                index++;
                assignments.Add(new RawSquadAssignment(member.Id, index));
            }
            
            Meta["Default:RawSquadAssignments_j"] = new RawSquadAssignments(assignments).ToString();
            await partyService.UpdateParty(authSession, Id, new PartyUpdate(this, new Dictionary<string, object>
            {
                {"Default:RawSquadAssignments_j", Meta["Default:RawSquadAssignments_j"]}
            }));
        }
        
        internal async Task UpdatePresence(XmppClient xmppClient)
        {
            object partyJoinInfo;
            var presencePermission = Meta["urn:epic:cfg:presence-perm_s"];

            if (presencePermission == "Noone" || presencePermission == "Leader" && Captain.Id != xmppClient.AuthSession.AccountId)
            {
                partyJoinInfo = new
                {
                    bIsPrivate = true
                };
            }
            else
            {
                partyJoinInfo = new PartyJoinInfo(
                    xmppClient.AuthSession.AccountId,
                    xmppClient.AuthSession.DisplayName,
                    xmppClient.CurrentParty.Id,
                    xmppClient.CurrentParty.Members.Count);
            }
            
            await xmppClient.SendPresence(new Presence(this, new Dictionary<string, object>
            {
                {"FortBasicInfo_j", new FortBasicInfo()},
                {"FortGameplayStats_j", new FortGameplayStats()},
                {"FortLFG_I", "0"},
                {"FortPartySize_i", 1},
                {"FortSubGame_i", 1},
                {"InUnjoinableMatch_b", false},
                {"party.joininfodata.286331153_j", partyJoinInfo}
            }));
        }
    }
}