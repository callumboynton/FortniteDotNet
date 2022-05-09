using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class RefundMtxPurchase
    {
        [JsonProperty("purchaseId")]
        public string PurchaseId { get; set; }

        [JsonProperty("quickReturn")]
        public bool QuickReturn { get; set; }
    }
}