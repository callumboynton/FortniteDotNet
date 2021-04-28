using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.Account
{
    public class AddExternalAuth
    {
        [JsonProperty("authType")]
        public string AuthType { get; set; }

        [JsonProperty("externalAuthToken")] 
        public string ExternalAuthToken { get; set; }
    }
}