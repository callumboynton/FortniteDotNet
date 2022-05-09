using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ApplyAlteration
    {
        [JsonProperty("alterationItemId")] 
        public string AlterationItemId { get; set; }

        [JsonProperty("targetItemId")]
        public string TargetItemId { get; set; }
    }
}