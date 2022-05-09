using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class RespecAlteration
    {
        [JsonProperty("targetItemId")]
        public string TargetItemId { get; set; }

        [JsonProperty("alterationSlot")]
        public string AlterationSlot { get; set; }

        [JsonProperty("alterationId")]
        public string AlterationId { get; set; }
    }
}