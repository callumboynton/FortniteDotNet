using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class AssignHeroToLoadout
    {
        [JsonProperty("slotName")]
        public string SlotName { get; set; }

        [JsonProperty("loadoutId")]
        public string LoadoutId { get; set; }

        [JsonProperty("heroId")]
        public string HeroId { get; set; }
    }
}