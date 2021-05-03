using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class CollectionBookPageAttributes
    {
        [JsonProperty("sectionStates")]
        public object[] SectionStates { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
    }
}