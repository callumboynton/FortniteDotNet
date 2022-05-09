using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class SetActiveHeroLoadout
    {
        [JsonProperty("selectedLoadout")]
        public string SelectedLoadout { get; set; }
    }
}