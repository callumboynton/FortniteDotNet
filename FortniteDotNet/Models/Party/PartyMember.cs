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
        public Dictionary<string, string> Meta { get; set; }

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
        public bool IsLeader => Role == "CAPTAIN";

        
        /// <summary>
        /// Updates the members properties.
        /// </summary>
        /// <param name="revision">The current revision the member is on.</param>
        /// <param name="updated">The updated meta of the member.</param>
        /// <param name="deleted">The deleted meta of the member.</param>
        public void UpdateMember(int revision, Dictionary<string, object> updated = null, IEnumerable<string> deleted = null)
        {
            if (revision > Revision)
                Revision = revision;
                
            foreach (var deletedMeta in deleted)
                Meta.Remove(deletedMeta);
            foreach (var updatedMeta in updated)
                Meta[updatedMeta.Key] = updatedMeta.Value.ToString();
        }

        /// <summary>
        /// Sets the outfit of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the outfit for.</param>
        /// <param name="outfit">The name of the desired outfit.</param>
        /// <param name="variantName">The name of the desired variant, this is optional.</param>
        public async Task SetOutfit(XMPPClient xmppClient, string outfit, string variantName = null)
        {
            Dictionary<string, object> patches = new();
                
            // Get the outfit by its name.
            var cosmetic = await CosmeticHelper.GetCosmeticByName(outfit, "AthenaCharacter");
            
            // If the user provided a variant name...
            if (variantName != null)
            {
                // Get the current loadout variants from the member meta.
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                
                // Get the desired variant.
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                    string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                
                // Set the athenaCharacter loadout to the desired variant.
                currentLoadoutVariants.Data.VariantLoadout["athenaCharacter"] = new
                {
                    i = new List<XMPP.Meta.Variant>
                    {
                        new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                            => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                    }
                };

                // Update the meta and add it to the patch.
                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }

            // Get the current loadout from the member meta, and set its character item definition to the desired outfit.
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.CharacterItemDefinition = $"/Game/Athena/Items/Cosmetics/Characters/{cosmetic.Id}.{cosmetic.Id}";
            
            // Update the meta and add it to the patch.
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
        }

        /// <summary>
        /// Sets the backpack of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the backpack for.</param>
        /// <param name="backpack">The name of the desired backpack.</param>
        /// <param name="variantName">The name of the desired variant, this is optional.</param>
        public async Task SetBackpack(XMPPClient xmppClient, string backpack, string variantName = null)
        {
            Dictionary<string, object> patches = new();
                
            // Get the backpack by its name.
            var cosmetic = await CosmeticHelper.GetCosmeticByName(backpack, "AthenaBackpack");
                
            // If the user provided a variant name...
            if (variantName != null)
            {
                // Get the current loadout variants from the member meta.
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                    
                // Get the desired variant.
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                    string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                    
                // Set the athenaBackpack loadout to the desired variant.
                currentLoadoutVariants.Data.VariantLoadout["athenaBackpack"] = new
                {
                    i = new List<XMPP.Meta.Variant>
                    {
                        new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                            => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                    }
                };

                // Update the meta and add it to the patch.
                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }

            // Get the current loadout from the member meta, and set its backpack item definition to the desired backpack.
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.BackpackItemDefinition = $"/Game/Athena/Items/Cosmetics/Backpacks/{cosmetic.Id}.{cosmetic.Id}";
            
            // Update the meta and add it to the patch.
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
                
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
        }

        
        /// <summary>
        /// Sets the pickaxe of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the pickaxe for.</param>
        /// <param name="pickaxe">The name of the desired pickaxe.</param>
        /// <param name="variantName">The name of the desired variant, this is optional.</param>
        public async Task SetPickaxe(XMPPClient xmppClient, string pickaxe, string variantName = null)
        {
            Dictionary<string, object> patches = new();
                
            // Get the pickaxe by its name.
            var cosmetic = await CosmeticHelper.GetCosmeticByName(pickaxe, "AthenaPickaxe");
                
            // If the user provided a variant name...
            if (variantName != null)
            {
                // Get the current loadout variants from the member meta.
                var currentLoadoutVariants = JsonConvert.DeserializeObject<AthenaCosmeticLoadoutVariants>(Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
                    
                // Get the desired variant.
                var variant = cosmetic.Variants.FirstOrDefault(x => x.Options.Any(x => 
                    string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)));
                    
                // Set the athenaPickaxe loadout to the desired variant.
                currentLoadoutVariants.Data.VariantLoadout["athenaPickaxe"] = new
                {
                    i = new List<XMPP.Meta.Variant>
                    {
                        new XMPP.Meta.Variant(variant.Channel, variant.Options.FirstOrDefault(x
                            => string.Equals(x.Name, variantName, StringComparison.CurrentCultureIgnoreCase)).Tag)
                    }
                };

                // Update the meta and add it to the patch.
                Meta["Default:AthenaCosmeticLoadoutVariants_j"] = currentLoadoutVariants.ToString();
                patches.Add("Default:AthenaCosmeticLoadoutVariants_j", Meta["Default:AthenaCosmeticLoadoutVariants_j"]);
            }

            // Get the current loadout from the member meta, and set its pickaxe item definition to the desired pickaxe.
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.PickaxeItemDefinition = $"/Game/Athena/Items/Cosmetics/Pickaxes/{cosmetic.Id}.{cosmetic.Id}";
                
            // Update the meta and add it to the patch.
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
            patches.Add("Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]);
                
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, patches);
        }

        /// <summary>
        /// Sets the emote of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the emote for.</param>
        /// <param name="emote">The name of the desired emote.</param>
        public async Task SetEmote(XMPPClient xmppClient, string emote)
        {
            // Get the emote by its name.
            var cosmetic = await CosmeticHelper.GetCosmeticByName(emote, "AthenaDance");

            // If the client is emoting, clear the emote.
            if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"]).Data.EmoteItemDefinition != "None")
                await ClearEmote(xmppClient);

            // Update the meta.
            Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, false, -2).ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            });
        }

        /// <summary>
        /// Sets the emoji of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the emoji for.</param>
        /// <param name="emoji">The name of the desired emoji.</param>
        public async Task SetEmoji(XMPPClient xmppClient, string emoji)
        {
            // Get the emoji by its name.
            var cosmetic = await CosmeticHelper.GetCosmeticByName(emoji, "AthenaEmoji");

            // If the client is emoting, clear the emote.
            if (JsonConvert.DeserializeObject<FrontendEmote>(Meta["Default:FrontendEmote_j"]).Data.EmoteItemDefinition != "None")
                await ClearEmote(xmppClient);

            // Update the meta.
            Meta["Default:FrontendEmote_j"] = new FrontendEmote(cosmetic.Id, true, -2).ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            });
        }

        /// <summary>
        /// Sets the banner for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the banner for.</param>
        /// <param name="bannerIcon">The desired banner icon.</param>
        /// <param name="bannerColor">The desired banner color.</param>
        public async Task SetBanner(XMPPClient xmppClient, string bannerIcon, string bannerColor)
        {
            // Get the current banner info from the member meta and update its properties.
            var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"]);
            currentInfo.Data.BannerIconId = bannerIcon;
            currentInfo.Data.BannerColorId = bannerColor;

            // Update the meta.
            Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"]}
            });
        }
        
        /// <summary>
        /// Sets the level for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the level for.</param>
        /// <param name="level">The desired level.</param>
        public async Task SetLevel(XMPPClient xmppClient, int level)
        {
            // Get the current banner info from the member meta and update its level.
            var currentInfo = JsonConvert.DeserializeObject<AthenaBannerInfo>(Meta["Default:AthenaBannerInfo_j"]);
            currentInfo.Data.SeasonLevel = level;

            // Update the meta.
            Meta["Default:AthenaBannerInfo_j"] = currentInfo.ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:AthenaBannerInfo_j", Meta["Default:AthenaBannerInfo_j"]}
            });
        }

        /// <summary>
        /// Sets the battle pass information for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to set the battle pass information for.</param>
        /// <param name="isPurchased">Is the battle pass purchased?</param>
        /// <param name="level">The desired level.</param>
        /// <param name="selfBoost">The desired personal XP boost.</param>
        /// <param name="friendBoost">The desired friend XP boost.</param>
        public async Task SetBattlePassInfo(XMPPClient xmppClient, bool isPurchased, int level, int selfBoost, int friendBoost)
        {
            // Get the current battle pass info from the member meta and update its properties.
            var currentInfo = JsonConvert.DeserializeObject<BattlePassInfo>(Meta["Default:BattlePassInfo_j"]);
            currentInfo.Data.HasPurchasedPass = isPurchased;
            currentInfo.Data.PassLevel = level;
            currentInfo.Data.PersonalXpBoost = selfBoost;
            currentInfo.Data.FriendXpBoost = friendBoost;

            // Update the meta.
            Meta["Default:BattlePassInfo_j"] = currentInfo.ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:BattlePassInfo_j", Meta["Default:BattlePassInfo_j"]}
            });
        }

        /// <summary>
        /// Clears the backpack for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to clear the backpack for.</param>
        public async Task ClearBackpack(XMPPClient xmppClient)
        {
            // Gets the current loadout from the member meta and sets its backpack item definition to "None".
            var currentLoadout = JsonConvert.DeserializeObject<AthenaCosmeticLoadout>(Meta["Default:AthenaCosmeticLoadout_j"]);
            currentLoadout.Data.BackpackItemDefinition = "None";
                
            // Update the meta.
            Meta["Default:AthenaCosmeticLoadout_j"] = currentLoadout.ToString();
                
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:AthenaCosmeticLoadout_j", Meta["Default:AthenaCosmeticLoadout_j"]}
            });
        }

        /// <summary>
        /// Clears the emote for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to clear the emote for.</param>
        public async Task ClearEmote(XMPPClient xmppClient)
        {
            // Reset the FrontendEmote property in the member meta.
            Meta["Default:FrontendEmote_j"] = new FrontendEmote().ToString();
            
            // Update the member.
            await PartyService.UpdateMember(xmppClient.AuthSession, this, xmppClient.CurrentParty.Id, new()
            {
                {"Default:FrontendEmote_j", Meta["Default:FrontendEmote_j"]}
            });
        }

        /// <inheritdoc cref="PartyService.KickMember"/>
        public async Task Kick(XMPPClient xmppClient)
            => await PartyService.KickMember(xmppClient, this);

        /// <inheritdoc cref="PartyService.PromoteMember"/>
        public async Task Promote(XMPPClient xmppClient)
            => await PartyService.PromoteMember(xmppClient, this);
        
        /// <summary>
        /// The default meta for a party member. Used when updating a member and there is no updated meta.
        /// </summary>
        internal static Dictionary<string, object> SchemaMeta
            => new()
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
        public Dictionary<string, string> Meta { get; set; }
    }
}