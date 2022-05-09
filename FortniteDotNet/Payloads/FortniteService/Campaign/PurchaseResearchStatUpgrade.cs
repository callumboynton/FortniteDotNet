using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class PurchaseResearchStatUpgrade
    {
        [JsonProperty("statId")]
        public string StatId { get; set; }
    }
}