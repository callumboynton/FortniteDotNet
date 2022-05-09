using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Storefront
{
    public class CatalogEntry
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }
        
        [JsonProperty("devName")]
        public string DevName { get; set; }

        [JsonProperty("offerType")]
        public string OfferType { get; set; }

        [JsonProperty("fulfillmentIds", NullValueHandling = NullValueHandling.Ignore)]
        public string[] FulfillmentIds { get; set; }

        [JsonProperty("dailyLimit")]
        public int DailyLimit { get; set; }

        [JsonProperty("weeklyLimit")]
        public int WeeklyLimit { get; set; }

        [JsonProperty("monthlyLimit")]
        public int MonthlyLimit { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        [JsonProperty("prices", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryPrice> Prices { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public object Meta { get; set; }

        [JsonProperty("matchFilter", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchFilter { get; set; }

        [JsonProperty("filterWeight", NullValueHandling = NullValueHandling.Ignore)]
        public double FilterWeight { get; set; }

        [JsonProperty("appStoreId")]
        public List<string> AppStoreId { get; set; }

        [JsonProperty("requirements", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryRequirements> Requirements { get; set; }

        [JsonProperty("refundable")]
        public bool Refundable { get; set; }

        [JsonProperty("giftInfo", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryGiftInfo GiftInfo { get; set; }

        [JsonProperty("metaInfo")]
        public List<CatalogEntryMetaInfo> MetaInfo { get; set; }

        [JsonProperty("displayAssetPath")]
        public string DisplayAssetPath { get; set; }

        [JsonProperty("itemGrants", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryItemGrant> ItemGrants { get; set; }

        [JsonProperty("catalogGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string CatalogGroup { get; set; }

        [JsonProperty("catalogGroupPriority")]
        public int CatalogGroupPriority { get; set; }

        [JsonProperty("sortPriority")]
        public int SortPriority { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("dynamicBundleInfo", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryBundleInfo DynamicBundleInfo { get; set; }

        [JsonProperty("additionalGrants", NullValueHandling = NullValueHandling.Ignore)]
        public string[] AdditionalGrants { get; set; }
    }
}