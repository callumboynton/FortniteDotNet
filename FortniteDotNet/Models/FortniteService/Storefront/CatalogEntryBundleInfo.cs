using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Storefront
{
    public class CatalogEntryBundleInfo
    {
        [JsonProperty("discountedBasePrice")]
        public int DiscountedBasePrice { get; set; }

        [JsonProperty("regularBasePrice")]
        public int RegularBasePrice { get; set; }

        [JsonProperty("floorPrice")]
        public int FloorPrice { get; set; }

        [JsonProperty("currencyType")]
        public string CurrencyType { get; set; }

        [JsonProperty("currencySubType")]
        public string CurrencySubType { get; set; }

        [JsonProperty("displayType")]
        public string DisplayType { get; set; }
        
        [JsonProperty("purchaseRequirements")]
        public List<CatalogEntryBundleItem> BundleItems { get; set; }
    }
}