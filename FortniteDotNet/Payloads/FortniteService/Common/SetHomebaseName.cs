using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class SetHomebaseName
    {
        [JsonProperty("homebaseName")]
        public string HomebaseName { get; set; }
    }
}