using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Enums.Fortnite;

namespace FortniteDotNet.Payloads.Fortnite
{
    public class GiftCatalogEntry
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }

        [JsonProperty("purchaseQuantity")]
        public int PurchaseQuantity { get; set; }

        [JsonProperty("currency")] 
        public string Currency { get; set; }

        [JsonProperty("currencySubType")] 
        public string CurrencySubType { get; set; }
        
        [JsonProperty("expectedTotalPrice")]
        public int ExpectedTotalPrice { get; set; }

        [JsonProperty("receiverAccountIds")]
        public string[] ReceiverAccountIds { get; set; }

        [JsonProperty("giftWrapTemplateId")]
        public GiftBox GiftWrapTemplateId { get; set; }

        [JsonProperty("personalMessage")]
        public string PersonalMessage { get; set; }
    }
}