using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class ItemAttributes : BaseAttributes
    {
        [JsonProperty("variants")]
        public List<ItemVariant> Variants { get; set; }
        
        [JsonProperty("rnd_sel_cnt")]
        public int RandomSelectionCount { get; set; }
    }
}