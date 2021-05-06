using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Storefront
{
    public class CatalogEntryGiftInfo
    {
        [JsonProperty("bIsEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("forcedGiftBoxTemplateId")]
        public string ForcedGiftBoxTemplateId { get; set; }

        [JsonProperty("purchaseRequirements")]
        public object[] PurchaseRequirements { get; set; }

        [JsonProperty("giftRecordIds", NullValueHandling = NullValueHandling.Ignore)]
        public string[] GiftRecordIds { get; set; }
    }
}