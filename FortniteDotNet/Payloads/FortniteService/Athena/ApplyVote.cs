using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class ApplyVote
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }
    }
}