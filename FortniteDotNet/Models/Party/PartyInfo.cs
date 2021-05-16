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
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }
        
        [JsonProperty("members")]
        public List<PartyMember> Members { get; set; }

        [JsonProperty("applicants")]
        public List<PartyMember> Applicants { get; set; }

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
        
        /// <summary>
        /// This updates the parties privacy.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="partyPrivacy">The desired privacy of the party.</param>
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

        /// <summary>
        /// Sets the custom matchmaking key for the current party with the provided key.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="key">The desired custom matchmaking key.</param>
        public async Task SetCustomMatchmakingKey(OAuthSession oAuthSession, string key = null)
        {
            Meta["Default:CustomMatchKey_s"] = key ?? "";

            await PartyService.UpdateParty(oAuthSession, this, new()
            {
                {"Default:CustomMatchKey_s", Meta["Default:CustomMatchKey_s"]}
            });
        }

        /// <summary>
        /// Sets the playlist for the current party with the provided playlist ID.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="playlistId">The playlist ID of the desired playlist.</param>
        public async Task SetPlaylist(OAuthSession oAuthSession, string playlistId)
        {
            Meta["Default:PlaylistData_j"] = new PlaylistDataMeta(playlistId).ToString();

            await PartyService.UpdateParty(oAuthSession, this, new()
            {
                {"Default:PlaylistData_j", Meta["Default:PlaylistData_j"]}
            });
        }

        /// <summary>
        /// This updates the parties squad assignments, mainly used when a member joins or leaves.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        internal async Task UpdateSquadAssignments(OAuthSession oAuthSession)
        {
            // Initialise our variables
            var assignments = new List<RawSquadAssignment>();
            var index = 0;
            
            // Add the current account to the list of assignments
            assignments.Add(new(oAuthSession.AccountId));

            // For each member in the party...
            foreach (var member in Members)
            {
                // If the member is our XMPP client, continue enumerating through the party's members.
                if (member.Id == oAuthSession.AccountId) 
                    continue;
                
                // Increment our index and add the member to the list of assignments.
                index++;
                assignments.Add(new(member.Id, index));
            }
            
            // Update/set our meta to the list of assignments.
            Meta["Default:RawSquadAssignments_j"] = new RawSquadAssignments(assignments).ToString();

            // Update the party with the new squad assignments.
            await PartyService.UpdateParty(oAuthSession, this, new()
            {
                {"Default:RawSquadAssignments_j", Meta["Default:RawSquadAssignments_j"]}
            });
        }
        
        /// <summary>
        /// Updates the party's properties.
        /// </summary>
        /// <param name="revision">The current revision the party is on.</param>
        /// <param name="config">The current config for the party.</param>
        /// <param name="updated">The updated party meta.</param>
        /// <param name="deleted">The deleted party meta.</param>
        internal void UpdateParty(int revision, Dictionary<string, object> config, Dictionary<string, object> updated = null, IEnumerable<string> deleted = null)
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

        /// <summary>
        /// This updates the presence for the provided <see cref="XMPPClient"/> based on the current party's privacy.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to update the presence for.</param>
        internal async Task UpdatePresence(XMPPClient xmppClient)
        {
            object partyJoinInfo;
            var presencePermission = Meta["urn:epic:cfg:presence-perm_s"];

            // If the party is private, don't send over the party join info.
            if (presencePermission == "Noone" || presencePermission == "Leader" && Leader.Id != xmppClient.AuthSession.AccountId)
            {
                partyJoinInfo = new
                {
                    bIsPrivate = true
                };
            }
            // Otherwise, send over the party join info.
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
                {"FortBasicInfo_j", new FortBasicInfo()},
                {"FortGameplayStats_j", new FortGameplayStats()},
                {"FortLFG_I", "0"},
                {"FortPartySize_i", 1},
                {"FortSubGame_i", 1},
                {"InUnjoinableMatch_b", false},
                {"party.joininfodata.286331153_j", partyJoinInfo}
            }));
        }

        /// <inheritdoc cref="PartyService.LeaveParty"/>
        public async Task Leave(XMPPClient xmppClient)
            => await PartyService.LeaveParty(xmppClient);
    }
}