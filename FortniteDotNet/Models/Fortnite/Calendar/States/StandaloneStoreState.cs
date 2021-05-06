using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Calendar.States
{
    public class StandaloneStoreState
    {
        [JsonProperty("activePurchaseLimitingEventIds")]
        public string[] ActivePurchaseLimitingEventIds { get; set; }

        [JsonProperty("storefront")]
        public object Storefront { get; set; }

        [JsonProperty("rmtPromotionConfig")]
        public string[] RMTPromotionConfig { get; set; }

        [JsonProperty("storeEnd")]
        public DateTime StoreEnd { get; set; }
    }
}