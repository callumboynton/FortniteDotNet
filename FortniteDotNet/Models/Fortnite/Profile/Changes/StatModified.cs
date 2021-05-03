using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Changes
{
    public class StatModified : BaseProfileChange
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}