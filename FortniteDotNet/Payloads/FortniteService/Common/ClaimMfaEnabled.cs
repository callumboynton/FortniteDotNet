using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class ClaimMfaEnabled
    {
        [JsonProperty("bClaimForStw")]
        public bool ClaimForStw { get; set; }
    }
}