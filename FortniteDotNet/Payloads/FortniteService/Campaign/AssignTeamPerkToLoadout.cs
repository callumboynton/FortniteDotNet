using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class AssignTeamPerkToLoadout
    {
        [JsonProperty("teamPerkId")]
        public string TeamPerkId { get; set; }

        [JsonProperty("loadoutId")]
        public string LoadoutId { get; set; }
    }
}