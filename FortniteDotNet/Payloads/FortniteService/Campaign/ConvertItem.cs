using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ConvertItem
    {
        [JsonProperty("targetItemId")]
        public string TargetItemId { get; set; }
    }
}