using Newtonsoft.Json;

namespace FortniteDotNet.Models.Party
{
    public class PartyJoined
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("party_id")]
        public string PartyId { get; set; }
    }
}