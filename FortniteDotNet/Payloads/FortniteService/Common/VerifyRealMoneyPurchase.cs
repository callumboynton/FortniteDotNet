using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class VerifyRealMoneyPurchase
    {
        [JsonProperty("appStore")]
        public string AppStore { get; set; } // EpicPurchasingService

        [JsonProperty("appStoreId")]
        public string AppStoreId { get; set; }

        [JsonProperty("receiptId")]
        public string ReceiptId { get; set; }

        [JsonProperty("receiptInfo")]
        public string ReceiptInfo { get; set; } // ENTITLEMENT

        [JsonProperty("purchaseCorrelationId")]
        public string PurchaseCorrelationId { get; set; }
    }
}