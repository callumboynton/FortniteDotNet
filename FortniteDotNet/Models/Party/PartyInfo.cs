using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Models.XMPP.Meta;
using FortniteDotNet.Models.XMPP.Payloads;

namespace FortniteDotNet.Models.Party
{
    public class PartyInfo
    {
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("applicants")]
        public List<string> Applicants { get; set; }
        
        [JsonProperty("members")]
        public List<PartyMember> Members { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("invites")]
        public List<PartyInvite> Invites { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("intentions")]
        public object[] Intentions { get; set; }

        [JsonIgnore] 
        public PartyMember Leader => Members.FirstOrDefault(x => x.Role == "CAPTAIN");

        public async Task UpdatePrivacy(OAuthSession oAuthSession, PartyPrivacy partyPrivacy)
        {
            Dictionary<string, object> updated = new();
            List<string> deleted = new();

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

            await PartyService.UpdateParty(oAuthSession, this, updated, deleted);
        }

        public async Task UpdateSquadAssignments(OAuthSession oAuthSession)
        {
            var assignments = new List<SquadAssignment>();
            var index = 0;
            
            assignments.Add(new(oAuthSession.AccountId));

            foreach (var member in Members)
            {
                if (member.Id == oAuthSession.AccountId) 
                    continue;
                
                index++;
                assignments.Add(new(member.Id, index));
            }

            Meta["Default:RawSquadAssignments_j"] = JsonConvert.SerializeObject(new
            {
                RawSquadAssignments = assignments
            });

            await PartyService.UpdateParty(oAuthSession, this, new()
            {
                { 
                    "Default:RawSquadAssignments_j", JsonConvert.SerializeObject(new
                    {
                        RawSquadAssignments = assignments
                    })
                }
            });
        }

        public void UpdateParty(int revision, Dictionary<string, object> config, Dictionary<string, object> updated = null, IEnumerable<string> deleted = null)
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
        }

        public async Task PatchPresence(XMPPClient xmppClient)
        {
            object partyJoinInfo;
            var presencePermission = Meta["urn:epic:cfg:presence-perm_s"];

            if (presencePermission == "Noone" || presencePermission == "Leader" && Leader.Id != xmppClient.AuthSession.AccountId)
            {
                partyJoinInfo = new
                {
                    bIsPrivate = true
                };
            }
            else
            {
                partyJoinInfo = new XMPP.Meta.PartyJoinInfo(
                    xmppClient.AuthSession.AccountId,
                    xmppClient.AuthSession.DisplayName,
                    xmppClient.CurrentParty.Id,
                    xmppClient.CurrentParty.Members.Count);
            }
            
            await xmppClient.SendPresence(new Presence(this, new()
            {
                {"party.joininfodata.286331153_j", partyJoinInfo},
                {"FortBasicInfo_j", new FortBasicInfo()},
                {"FortLFG_I", "0"},
                {"FortPartySize_i", 1},
                {"FortSubGame_i", 1},
                {"InUnjoinableMatch_b", false},
                {"FortGameplayStats_j", new FortGameplayStats()}
            }));
        }
    }
}