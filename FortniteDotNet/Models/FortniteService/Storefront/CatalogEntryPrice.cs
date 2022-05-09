using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Storefront
{
    public class CatalogEntryPrice
    {
        [JsonProperty("currencyType")]
        public string CurrencyType { get; set; }

        [JsonProperty("currencySubType")]
        public string CurrencySubType { get; set; }

        [JsonProperty("regularPrice")]
        public int RegularPrice { get; set; }

        [JsonProperty("dynamicRegularPrice")]
        public int DynamicRegularPrice { get; set; }

        [JsonProperty("finalPrice")]
        public int FinalPrice { get; set; }

        [JsonProperty("saleExpiration")]
        public DateTime SaleExpiration { get; set; }

        [JsonProperty("basePrice")]
        public int BasePrice { get; set; }
    }
}