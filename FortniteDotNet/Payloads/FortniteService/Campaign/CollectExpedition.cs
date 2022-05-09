using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class CollectExpedition
    {
        [JsonProperty("expeditionId")]
        public string ExpeditionId { get; set; }
        
        [JsonProperty("expeditionTemplate")]
        public string ExpeditionTemplate { get; set; }
    }
}