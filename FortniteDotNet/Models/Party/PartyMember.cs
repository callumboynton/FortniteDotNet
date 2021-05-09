using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.Common;

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

        public void UpdateMember(int revision, Dictionary<string, object> updated = null, List<string> deleted = null)
        {
            if (revision > Revision)
                Revision = revision;
                
            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);

            foreach (var updatedMeta in updated)
                Meta[updatedMeta.Key] = updatedMeta.Value.ToString();
        }

        internal static Dictionary<string, object> SchemaMeta
            => new()
            {
                { "Default:Location_s", "PreLobby" },
                { "Default:CampaignHero_j", JsonConvert.SerializeObject(new
                    {
                        CampaignHero = new
                        {
                            heroItemInstanceId = "",
                            heroType = "FortHeroType'/Game/Athena/Heroes/CID_028_Athena_Commando_F.CID_028_Athena_Commando_F'"
                        }
                    }) 
                },
                { "Default:MatchmakingLevel_U" , "0" },
                { "Default:ZoneInstanceId_s", "" },
                { "Default:HomeBaseVersion_U", "1" },
                { "Default:HasPreloadedAthena_b", "false" },
                { "Default:FrontendEmote_j", JsonConvert.SerializeObject(new
                    {
                        FrontendEmote = new
                        {
                            emoteItemDef = "None",
                            emoteEKey = "",
                            emoteSection = -1
                        }
                    }) 
                },
                { "Default:NumAthenaPlayersLeft_U", "0" },
                { "Default:Utc:timeStartedMatchAthena_s", "0001-01-01T00:00:00.000Z" },
                { "Default:GameReadiness_s", "NotReady" },
                { "Default:HiddenMatchmakingDelayMax_U", "0" },
                { "Default:ReadyInputType_s", "Count" },
                { "Default:CurrentInputType_s", "MouseAndKeyboard" },
                { "Default:AssistedChallengeInfo_j", JsonConvert.SerializeObject(new
                    {
                        AssistedChallengeInfo = new
                        {
                            questItemDef = "None",
                            objectivesCompleted = 0
                        }
                    })
                },
                { "Default:MemberSquadAssignmentRequest_j", JsonConvert.SerializeObject(new
                    {
                        MemberSquadAssignmentRequest = new
                        {
                            startingAbsoluteIdx = -1,
                            targetAbsoluteIdx = -1,
                            swapTargetMemberId = "INVALID",
                            version = 0
                        }
                    })
                },
                { "Default:AthenaCosmeticLoadout_j", JsonConvert.SerializeObject(new
                    {
                        AthenaCosmeticLoadout = new
                        {
                            characterDef = "AthenaCharacterItemDefinition'/Game/Athena/Items/Cosmetics/Characters/CID_028_Athena_Commando_F.CID_028_Athena_Commando_F'",
                            characterEKey = "",
                            backpackDef = "None",
                            backpackEKey = "",
                            pickaxeDef = "AthenaPickaxeItemDefinition\'/Game/Athena/Items/Cosmetics/Pickaxes/DefaultPickaxe.DefaultPickaxe\'",
                            pickaxeEKey = "",
                            contrailDef = "None",
                            contrailEKey = "",
                            scratchpad = Array.Empty<object>()
                        }
                    })
                },
                { "Default:AthenaCosmeticLoadoutVariants_j", JsonConvert.SerializeObject(new
                    {
                        AthenaCosmeticLoadoutVariants = new
                        {
                            vL = new { }
                        }
                    })
                },
                { "Default:AthenaBannerInfo_j", JsonConvert.SerializeObject(new
                    {
                        AthenaBannerInfo = new
                        {
                            bannerIconId = "StandardBanner15",
                            bannerColorId = "DefaultColor15",
                            seasonLevel = 1
                        }
                    })
                },
                { "Default:BattlePassInfo_j", JsonConvert.SerializeObject(new
                    {
                        BattlePassInfo = new
                        {
                            bHasPurchasedPass = false,
                            passLevel = 1,
                            selfBoostXp = 0,
                            friendBoostXp = 0
                        }
                    })
                },
                { "Default:Platform_j", JsonConvert.SerializeObject(new
                    {
                        Platform = new
                        {
                            platformStr = "WIN"
                        }
                    })
                },
                { "Default:PlatformUniqueId_s", "INVALID" },
                { "Default:PlatformSessionId_s", "" },
                { "Default:CrossplayPreference_s", "OptedIn" },
                { "Default:VoiceChatEnabled_b", "true" },
                { "Default:VoiceConnectionId_s", "" },
                { "Default:SpectateAPartyMemberAvailable_b", "false" },
                { "Default:FeatDefinition_s", "None" },
                { "Default:VoiceChatStatus_s", "Disabled" }
            };
    }

    public class PartyMemberConnection
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("connected_at")]
        public DateTime ConnectedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("yield_leadership")]
        public bool YieldLeadership { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }
    }
}