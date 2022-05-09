using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Changes
{
    public class BaseProfileChange
    {
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }
    }
}