using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class WorldItemAttributes
    {
        [JsonProperty("loadedAmmo")]
        public int LoadedAmmo { get; set; }
        
        [JsonProperty("inventory_overflow_date")]
        public bool InventoryOverflowDate { get; set; }
        
        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("alterationDefinitions")]
        public List<string> AlterationDefinitions { get; set; }
        
        [JsonProperty("durability")]
        public double Durability { get; set; }
        
        [JsonProperty("itemSource")]
        public string ItemSource { get; set; }
    }
}