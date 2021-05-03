using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class BaseAttributes
    {
        [JsonProperty("max_level_bonus")]
        public int MaxLevelBonus { get; set; }
        
        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("item_seen")]
        public bool ItemSeen { get; set; }
        
        [JsonProperty("xp")]
        public int XP { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
    }
}