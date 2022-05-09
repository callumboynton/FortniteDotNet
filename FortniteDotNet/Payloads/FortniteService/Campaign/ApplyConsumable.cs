using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ApplyConsumable
    {
        [JsonProperty("targetItemId")]
        public string TargetItemId { get; set; }
    }
}