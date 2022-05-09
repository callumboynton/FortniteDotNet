using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class PurchaseCatalogEntry
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }

        [JsonProperty("purchaseQuantity")]
        public int PurchaseQuantity { get; set; }

        [JsonProperty("currency")] 
        public string Currency { get; set; }
        
        [JsonProperty("expectedTotalPrice")]
        public int ExpectedTotalPrice { get; set; }
        
        [JsonProperty("gameContext")]
        public string GameContext { get; set; }
    }
}