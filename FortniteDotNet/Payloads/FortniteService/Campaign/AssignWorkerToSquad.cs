using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class AssignWorkerToSquad
    {
        [JsonProperty("squadId")]
        public string SquadId { get; set; }

        [JsonProperty("characterId")]
        public string CharacterId { get; set; }
    }
}