using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class CollectionBookPageAttributes
    {
        [JsonProperty("sectionStates")]
        public object[] SectionStates { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
    }
}