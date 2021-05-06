using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Storefront
{
    public class Catalog
    {
        [JsonProperty("refreshIntervalHrs")]
        public int RefreshIntervalHrs { get; set; }

        [JsonProperty("dailyPurchaseHrs")]
        public int DailyPurchaseHrs { get; set; }

        [JsonProperty("expiration")] 
        public DateTime Expiration { get; set; }
        
        [JsonProperty("storefronts")]
        public List<Storefront> Storefronts { get; set; }
    }

    public class Storefront
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("catalogEntries")]
        public List<CatalogEntry> CatalogEntries { get; set; }
    }
}