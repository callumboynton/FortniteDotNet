using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FortniteDotNet.Models.Fortnite.Profile.Attributes;

namespace FortniteDotNet.Models.Fortnite.Profile
{
    public class ProfileItem
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }
        
        [JsonProperty("attributes")]
        public object Attributes { get; set; }
        
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public ProfileItem(string templateId, JObject attributes)
        {
            switch (templateId.Split(":")[0])
            {
                case "AthenaCharacter":
                case "AthenaBackpack":
                case "AthenaPickaxe":
                case "AthenaGlider":
                case "AthenaSkyDiveContrail":
                case "AthenaDance":
                case "AthenaItemWrap":
                case "AthenaLoadingScreen":
                case "AthenaMusicPack":
                case "Token":
                case "HomebaseBannerIcon":
                case "HomebaseBannerColor":
                case "BannerToken":
                    Attributes = attributes.ToObject<ItemAttributes>();
                    return;
                case "Accolades":
                    Attributes = attributes.ToObject<AccoladeAttributes>();
                    return;
                case "ChallengeBundle":
                    Attributes = attributes.ToObject<ChallengeBundleAttributes>();
                    return;
                case "ChallengeBundleCompletionToken":
                    Attributes = attributes.ToObject<ChallengeBundleCompletionTokenAttributes>();
                    return;
                case "ChallengeBundleSchedule":
                    Attributes = attributes.ToObject<ChallengeBundleScheduleAttributes>();
                    return;
                case "CollectableCharacter":
                    Attributes = attributes.ToObject<CollectableCharacterAttributes>();
                    return;
                case "CollectableFish":
                    Attributes = attributes.ToObject<CollectableFishAttributes>();
                    return;
                case "CollectionBookPage":
                    Attributes = attributes.ToObject<CollectionBookPageAttributes>();
                    return;
                case "ConditionalAction":
                    Attributes = attributes.ToObject<ConditionalActionAttributes>();
                    return;
                case "CosmeticLocker":
                    Attributes = attributes.ToObject<CosmeticLockerAttributes>();
                    return;
                case "CosmeticVariantToken":
                    Attributes = attributes.ToObject<CosmeticVariantTokenAttributes>();
                    return;
                case "CreativePlot":
                    Attributes = attributes.ToObject<CreativePlotAttributes>();
                    return;
                case "Currency":
                    Attributes = attributes.ToObject<CurrencyAttributes>();
                    return;
                case "Defender":
                    Attributes = attributes.ToObject<DefenderAttributes>();
                    return;
                case "EventPurchaseTracker":
                    Attributes = attributes.ToObject<EventPurchaseTrackerAttributes>();
                    return;
                case "Hero":
                    Attributes = attributes.ToObject<HeroAttributes>();
                    return;
                case "Quest":
                    Attributes = attributes.ToObject<QuestAttributes>();
                    return;
                case "RepeatableDailiesCard":
                    Attributes = attributes.ToObject<RepeatableDailiesCardAttributes>();
                    return;
                case "RewardEventGraphPurchaseToken":
                    Attributes = attributes.ToObject<RewardEventGraphPurchaseTokenAttributes>();
                    return;
                case "Schematic":
                    Attributes = attributes.ToObject<SchematicAttributes>();
                    return;
                case "Weapon":
                    Attributes = attributes.ToObject<WeaponAttributes>();
                    return;
                case "Worker":
                    Attributes = attributes.ToObject<WorkerAttributes>();
                    return;
                default:
                    Attributes = attributes.ToObject<BaseAttributes>();
                    return;
            }
        }
    }
}