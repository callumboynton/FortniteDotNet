using System;
using System.Linq;
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

        public async Task SetOutfit(XMPPClient xmppClient, string outfit, string variantName = null)
        {
            try
            {
                Dictionary<string, object> patches = new();
                
                var cosmetic = await CosmeticHelper.GetCosmeticByName(outfit, "AthenaCharacter");
                
                if (variantName != null)
                {
                    var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"].ToString());
                    
                    var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                        string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                    
                    currentLoadoutVariants.Data.VariantLoadout["athenaCharacter"] = new
                    {
                        i = new List<XMPP.Meta.Variant>
                        {
                            new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                                => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                        }
                    };

                    Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                    patches.Add("Default:AthenaCosmeticLoadoutVariants_j", currentLoadoutVariants.ToString());
                }

                var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"].ToString());
                currentLoadout.Data.CharacterItemDefinition = $"/Game/Athena/Items/Cosmetics/Characters/{cosmetic.Id}.{cosmetic.Id}";
                
                Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
                patches.Add("Default:AthenaCosmeticLoadout_j", currentLoadout.ToString());
                
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
                
                await xmppClient.SendMessage($"Set outfit to {cosmetic.Name}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task SetBackpack(XMPPClient xmppClient, string backpack, string variantName = null)
        {
            try
            {
                Dictionary<string, object> patches = new();
                
                var cosmetic = await CosmeticHelper.GetCosmeticByName(backpack, "AthenaBackpack");
                
                if (variantName != null)
                {
                    var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"].ToString());
                    
                    var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                        string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                    
                    currentLoadoutVariants.Data.VariantLoadout["athenaBackpack"] = new
                    {
                        i = new List<XMPP.Meta.Variant>
                        {
                            new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                                => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                        }
                    };

                    Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                    patches.Add("Default:AthenaCosmeticLoadoutVariants_j", currentLoadoutVariants.ToString());
                }

                var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"].ToString());
                currentLoadout.Data.BackpackItemDefinition = $"/Game/Athena/Items/Cosmetics/Backpacks/{cosmetic.Id}.{cosmetic.Id}";
                
                Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
                patches.Add("Default:AthenaCosmeticLoadout_j", currentLoadout.ToString());
                
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
                
                await xmppClient.SendMessage($"Set backbling to {cosmetic.Name}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task SetPickaxe(XMPPClient xmppClient, string pickaxe, string variantName = null)
        {
            try
            {
                Dictionary<string, object> patches = new();
                
                var cosmetic = await CosmeticHelper.GetCosmeticByName(pickaxe, "AthenaPickaxe");
                
                if (variantName != null)
                {
                    var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"].ToString());
                    
                    var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                        string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                    
                    currentLoadoutVariants.Data.VariantLoadout["athenaPickaxe"] = new
                    {
                        i = new List<XMPP.Meta.Variant>
                        {
                            new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                                => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                        }
                    };

                    Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                    patches.Add("Default:AthenaCosmeticLoadoutVariants_j", currentLoadoutVariants.ToString());
                }

                var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"].ToString());
                currentLoadout.Data.PickaxeItemDefinition = $"/Game/Athena/Items/Cosmetics/Pickaxes/{cosmetic.Id}.{cosmetic.Id}";
                
                Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
                patches.Add("Default:AthenaCosmeticLoadout_j", currentLoadout.ToString());
                
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
                
                await xmppClient.SendMessage($"Set pickaxe to {cosmetic.Name}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task SetEmote(XMPPClient xmppClient, string emote)
        {
            try
            {
                var cosmetic = await CosmeticHelper.GetCosmeticByName(emote, "AthenaDance");

                if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"].ToString()).Data.EmoteItemDefinition != "None")
                    await ClearEmote(xmppClient);

                Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, false, -2).ToString();
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

        public async Task SetEmoji(XMPPClient xmppClient, string emote)
        {
            try
            {
                var cosmetic = await CosmeticHelper.GetCosmeticByName(emote, "AthenaEmoji");

                if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"].ToString()).Data.EmoteItemDefinition != "None")
                    await ClearEmote(xmppClient);

                Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, true, -2).ToString();
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

        public async Task SetBanner(XMPPClient xmppClient, string bannerIcon, string bannerColor)
        {
            try
            {
                var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"].ToString());
                currentInfo.Data.BannerIconId = bannerIcon;
                currentInfo.Data.BannerColorId = bannerColor;

                Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"].ToString()}
                });
                
                await xmppClient.SendMessage($"Set banner icon to {bannerIcon} and banner color to {bannerColor}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task SetLevel(XMPPClient xmppClient, int level)
        {
            try
            {
                var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"].ToString());
                currentInfo.Data.SeasonLevel = level;

                Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"].ToString()}
                });
                
                await xmppClient.SendMessage($"Set level to {level}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task SetBattlePassInfo(XMPPClient xmppClient, bool isPurchased, int level, int selfBoost, int friendBoost)
        {
            try
            {
                var currentInfo = JsonConvert.DeserializeObject<BattlePassInfo>(Meta["Default:BattlePassInfo_j"].ToString());
                currentInfo.Data.HasPurchasedPass = isPurchased;
                currentInfo.Data.PassLevel = level;
                currentInfo.Data.PersonalXpBoost = selfBoost;
                currentInfo.Data.FriendXpBoost = friendBoost;

                Meta["Default:BattlePassInfo_j"] = currentInfo.ToString();
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:BattlePassInfo_j", Meta["Default:BattlePassInfo_j"].ToString()}
                });
                
                await xmppClient.SendMessage($"Set level to {level}", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task ClearBackpack(XMPPClient xmppClient)
        {
            try
            {
                var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"].ToString());
                currentLoadout.Data.BackpackItemDefinition = "None";
                
                Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
                
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"].ToString()}
                });
                
                await xmppClient.SendMessage("Cleared backbling", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
            catch (Exception ex)
            {
                await xmppClient.SendMessage(ex.Message, $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
            }
        }

        public async Task ClearEmote(XMPPClient xmppClient)
        {
            try
            {
                Meta["Default:FrontendEmote_j"] = new FrontendEmote().ToString();
                await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
                {
                    {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"].ToString()}
                });
                
                await xmppClient.SendMessage("Cleared emote", $"Party-{xmppClient.CurrentParty.Id}@muc.prod.ol.epicgames.com", true);
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