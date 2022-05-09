using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class PurchaseOrUpgradeHomebaseNode
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
    }
}