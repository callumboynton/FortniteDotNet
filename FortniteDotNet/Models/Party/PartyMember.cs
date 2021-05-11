using System;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Models.XMPP.Meta;

namespace FortniteDotNet.Models.Party
{
    public class PartyMember
    {
        [JsonProperty("account_id")]
        public string Id { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }

        [JsonProperty("connections", NullValueHandling = NullValueHandling.Ignore)]
        public List<PartyMemberConnection> Connections { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonIgnore] 
        public bool IsCaptain => Role == "CAPTAIN";

        public void UpdateMember(int revision, Dictionary<string, object> updated = null, IEnumerable<string> deleted = null)
        {
            if (revision > Revision)
                Revision = revision;
                
            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);
            foreach (var updatedMeta in updated)
                Meta[updatedMeta.Key] = updatedMeta.Value.ToString();
        }

        public async Task SetEmote(XMPPClient xmppClient, string emote)
        {
            try
            {
                var cosmetic = await CosmeticHelper.GetCosmeticByName(emote, "AthenaDance");

                if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"].ToString()).Data.EmoteItemDefinition != "None")
                {
                    Meta["Default:FrontendEmote_j"] = new FrontendEmote().ToString();
                    await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                    {
                        {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"].ToString()}
                    });
                }

                Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, -2).ToString();
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"].ToString()}
                });
                
                await xmppClient.SendMessage($"Set emote to {cosmetic.Name}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        internal static Dictionary<string, object> SchemaMeta
            => new()
            {
                {"Default:Location_s", "PreLobby"},
                {"Default:CampaignHero_j", new CampaignHero("CID_027_Athena_Commando_F").ToString()},
                // CampaignInfo
                {"Default:FrontendEmote_j", new FrontendEmote().ToString()},
                {"Default:NumAthenaPlayersLeft_U", "0"},
                {"Default:SpectateAPartyMemberAvailable_b", "false"},
                {"Default:Utc:timeStartedMatchAthena_s", "0001-01-01T00:00:00.000Z"},
                {"Default:LobbyState_j", new LobbyState().ToString()},
                {"Default:AssistedChallengeInfo_j", new AssistedChallengeInfo().ToString()},
                {"Default:FeatDefinition_s", "None"},
                {"Default:MemberSquadAssignmentRequest_j", new SquadAssignmentRequest().ToString()},
                {"Default:VoiceChatStatus_s", "Disabled"},
                {"Default:SidekickStatus_s", "None"},
                {"Default:AthenaCosmeticLoadout_j", new AthenaCosmeticLoadout("CID_027_Athena_Commando_F").ToString()},
                {"Default:AthenaCosmeticLoadoutVariants_j", new AthenaCosmeticLoadoutVariants().ToString()},
                {"Default:AthenaBannerInfo_j", new AthenaBannerInfo("brseason01", "defaultcolor19", 69).ToString()},
                {"Default:BattlePassInfo_j", new BattlePassInfo(100, 0, 0).ToString()},
                {"Default:PlatformData_j", new PlatformMeta().ToString()},
                {"Default:CrossplayPreference_s", "OptedIn"}
            };
    }

    public class PartyMemberConnection
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("connected_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ConnectedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("yield_leadership", NullValueHandling = NullValueHandling.Ignore)]
        public bool YieldLeadership { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }
    }
}