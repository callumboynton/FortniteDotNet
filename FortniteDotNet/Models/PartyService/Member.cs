using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FortniteDotNet.Enums.PartyService;
using FortniteDotNet.Services;
using FortniteDotNet.Xmpp;
using FortniteDotNet.Xmpp.Meta;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class Member
    {
        [JsonProperty("account_id")]
        public string Id { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonIgnore] 
        public bool IsCaptain => Role == Role.CAPTAIN;
        
        internal void Update(int revision, Dictionary<string, object> updated, List<string> deleted = null)
        {
            if (revision > Revision)
                Revision = revision;
                
            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);
            foreach (var updatedMeta in updated)
                Meta[updatedMeta.Key] = updatedMeta.Value.ToString();

            UpdatedAt = DateTime.UtcNow;
        }
        
        public async Task SetOutfit(IPartyService partyService, XmppClient xmppClient, string outfit, string variantName = null)
        {
            var patches = new Dictionary<string, object>();
            var cosmetic = await Tools.GetCosmeticByName(outfit, "AthenaCharacter");
            
            if (variantName != null)
            {
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(y => string.Equals(y.Name, variantName, StringComparison.OrdinalIgnoreCase)));
                
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                currentLoadoutVariants.Data.VariantLoadout["athenaCharacter"] = new
                {
                    i = new List<Variant>
                    {
                        new Variant(variant.Channel, variant.Options.FirstOrDefault(x => string.Equals(x.Name, variantName, StringComparison.OrdinalIgnoreCase)).Tag)
                    }
                };
                
                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }
            
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.CharacterItemDefinition = $"/Game/Athena/Items/Cosmetics/Characters/{cosmetic.Id}.{cosmetic.Id}";
            
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, patches));
        }
        
        public async Task SetBackpack(IPartyService partyService, XmppClient xmppClient, string backpack, string variantName = null)
        {
            var patches = new Dictionary<string, object>();
            var cosmetic = await Tools.GetCosmeticByName(backpack, "AthenaBackpack");
            
            if (variantName != null)
            {
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(y => string.Equals(y.Name, variantName, StringComparison.OrdinalIgnoreCase)));
                
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                currentLoadoutVariants.Data.VariantLoadout["athenaBackpack"] = new
                {
                    i = new List<Variant>
                    {
                        new Variant(variant.Channel, variant.Options.FirstOrDefault(x => string.Equals(x.Name, variantName, StringComparison.OrdinalIgnoreCase)).Tag)
                    }
                };
                
                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }
            
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.BackpackItemDefinition = $"/Game/Athena/Items/Cosmetics/Backpacks/{cosmetic.Id}.{cosmetic.Id}";
            
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, patches));
        }
        
        public async Task SetPickaxe(IPartyService partyService, XmppClient xmppClient, string pickaxe, string variantName = null)
        {
            var patches = new Dictionary<string, object>();
            var cosmetic = await Tools.GetCosmeticByName(pickaxe, "AthenaPickaxe");
            
            if (variantName != null)
            {
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(y => 
                    string.Equals(y.Name, variantName, StringComparison.OrdinalIgnoreCase)));
                
                currentLoadoutVariants.Data.VariantLoadout["athenaPickaxe"] = new
                {
                    i = new List<Variant>
                    {
                        new Variant(variant.Channel, variant.Options.FirstOrDefault(x => string.Equals(x.Name, variantName, StringComparison.OrdinalIgnoreCase)).Tag)
                    }
                };

                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }

            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.PickaxeItemDefinition = $"/Game/Athena/Items/Cosmetics/Pickaxes/{cosmetic.Id}.{cosmetic.Id}";
            
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, patches));
        }
        
        public async Task SetEmote(IPartyService partyService, XmppClient xmppClient, string emote)
        {
            var cosmetic = await Tools.GetCosmeticByName(emote, "AthenaDance");
            
            if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"]).Data.EmoteItemDefinition != "None")
                await ClearEmote(partyService, xmppClient);
            
            Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, false, -2).ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            }));
        }
        
        public async Task SetEmoji(IPartyService partyService, XmppClient xmppClient, string emoji)
        {
            var cosmetic = await Tools.GetCosmeticByName(emoji, "AthenaEmoji");
            
            if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"]).Data.EmoteItemDefinition != "None")
                await ClearEmote(partyService, xmppClient);
            
            Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, true, -2).ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            }));
        }
        
        public async Task SetBanner(IPartyService partyService, XmppClient xmppClient, string bannerIcon = "BRSeason01", string bannerColor = "DefaultColor0")
        {
            var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"]);
            currentInfo.Data.BannerIconId = bannerIcon;
            currentInfo.Data.BannerColorId = bannerColor;

            Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"]}
            }));
        }
        
        public async Task SetLevel(IPartyService partyService, XmppClient xmppClient, int level = 100)
        {
            var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"]);
            currentInfo.Data.SeasonLevel = level;

            Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"]}
            }));
        }
        
        public async Task SetBattlePassInfo(IPartyService partyService, XmppClient xmppClient, bool isPurchased = true, int level = 100, int selfBoost = 120, int friendBoost = 40)
        {
            var currentInfo = JsonConvert.DeserializeObject<BattlePassInfo>(Meta["Default:BattlePassInfo_j"]);
            currentInfo.Data.HasPurchasedPass = isPurchased;
            currentInfo.Data.PassLevel = level;
            currentInfo.Data.PersonalXpBoost = selfBoost;
            currentInfo.Data.FriendXpBoost = friendBoost;

            Meta["Default:BattlePassInfo_j"] = currentInfo.ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:BattlePassInfo_j", Meta["Default:BattlePassInfo_j"]}
            }));
        }
        
        public async Task ClearBackpack(IPartyService partyService, XmppClient xmppClient)
        {
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.BackpackItemDefinition = "None";
            
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]}
            }));
        }
        
        public async Task ClearEmote(IPartyService partyService, XmppClient xmppClient)
        {
            Meta["Default:FrontendEmote_j"] = new FrontendEmote().ToString();
            await partyService.UpdateMember(xmppClient.AuthSession, xmppClient.CurrentParty.Id, Id, new MemberUpdate(this, new Dictionary<string, object>
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            }));
        }
        
        internal static Dictionary<string, object> SchemaMeta
            => new Dictionary<string, object>
            {
                // TODO: un-hard code these properties.
                {"Default:Location_s", "PreLobby"},
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
                {"Default:AthenaCosmeticLoadout_j", new AthenaCosmeticLoadout("CID_028_Athena_Commando_F").ToString()},
                {"Default:AthenaCosmeticLoadoutVariants_j", new AthenaCosmeticLoadoutVariants("athenaCharacter", "Material", "Mat2").ToString()},
                {"Default:AthenaBannerInfo_j", new AthenaBannerInfo("brseason01", "defaultcolor19", 69).ToString()},
                {"Default:BattlePassInfo_j", new BattlePassInfo(true, 100, 0, 0).ToString()},
                {"Default:PlatformData_j", new PlatformMeta().ToString()},
                {"Default:CrossplayPreference_s", "OptedIn"}
            };
    }

    public class Connection
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
        public Dictionary<string, string> Meta { get; set; }
    }
}