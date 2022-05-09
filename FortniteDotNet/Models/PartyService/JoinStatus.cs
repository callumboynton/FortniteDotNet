using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class JoinStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("party_id")]
        public string PartyId { get; set; }
    }
}