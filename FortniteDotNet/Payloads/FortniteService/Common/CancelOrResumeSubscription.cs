using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class CancelOrResumeSubscription
    {
        [JsonProperty("appStore")]
        public string AppStore { get; set; }

        [JsonProperty("uniqueSubscriptionId")]
        public string UniqueSubscriptionId { get; set; }

        [JsonProperty("willAutoRenew")]
        public bool WillAutoRenew { get; set; }
    }
}