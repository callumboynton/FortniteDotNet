using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ConvertLegacyAlterations
    {
        [JsonProperty("targetItemId")]
        public string TargetItemId { get; set; }
    }
}