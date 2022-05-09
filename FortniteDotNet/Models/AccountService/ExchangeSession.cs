using Newtonsoft.Json;

namespace FortniteDotNet.Models.AccountService
{
    public class ExchangeSession
    {
        [JsonProperty("expiresInSeconds")] 
        public int ExpiresInSeconds { get; set; }

        [JsonProperty("creatingClientId")] 
        public string CreatingClientId { get; set; }

        [JsonProperty("code")] 
        public string Code { get; set; }
    }
}