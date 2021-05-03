using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Changes
{
    public class BaseProfileChange
    {
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }
    }
}