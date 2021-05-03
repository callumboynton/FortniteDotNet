using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class ItemAttributes : BaseAttributes
    {
        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; }
        
        [JsonProperty("rnd_sel_cnt")]
        public int RandomSelectionCount { get; set; }
    }
    
    public class Variant
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        
        [JsonProperty("active")]
        public string Active { get; set; }
        
        [JsonProperty("owned")]
        public List<string> Owned { get; set; }
    }
}