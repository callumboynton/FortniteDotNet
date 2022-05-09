using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class AbandonExpedition
    {
        [JsonProperty("expeditionId")]
        public string ExpeditionId { get; set; }
    }
}