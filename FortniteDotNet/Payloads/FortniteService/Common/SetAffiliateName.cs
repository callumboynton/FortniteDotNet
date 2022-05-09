using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class SetAffiliateName
    {
        [JsonProperty("affiliateName")]
        public string AffiliateName { get; set; }
    }
}