using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite
{
    public class Receipt
    {
        [JsonProperty("appStore")]
        public string AppStore { get; set; }

        [JsonProperty("appStoreId")]
        public string AppStoreId { get; set; }

        [JsonProperty("receiptId")]
        public string ReceiptId { get; set; }

        [JsonProperty("receiptInfo")]
        public string ReceiptInfo { get; set; }
    }
}