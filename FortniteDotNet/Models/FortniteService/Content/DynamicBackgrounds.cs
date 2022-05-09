using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class DynamicBackgrounds : BasePagesEntry
    {
        [JsonProperty("backgrounds")]
        public DynamicBackgroundList Backgrounds { get; set; }
    }

    public class DynamicBackgroundList
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
        
        [JsonProperty("backgrounds")]
        public List<DynamicBackground> Backgrounds { get; set; }
    }

    public class DynamicBackground
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
        
        [JsonProperty("stage")]
        public string Stage { get; set; }
        
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}