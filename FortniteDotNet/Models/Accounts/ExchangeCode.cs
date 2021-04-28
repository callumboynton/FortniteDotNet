using Newtonsoft.Json;

namespace FortniteDotNet.Models.Accounts
{
    public class ExchangeCode
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("creatingClientId")]
        public string CreatingClientId { get; set; }

        [JsonProperty("expiresInSeconds")]
        public int ExpiresInSeconds { get; set; }
    }
}