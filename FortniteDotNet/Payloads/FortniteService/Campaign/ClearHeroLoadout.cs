using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ClearHeroLoadout
    {
        [JsonProperty("loadoutId")]
        public string LoadoutId { get; set; }
    }
}