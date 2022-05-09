using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class SubGameInfoEntry : BasePagesEntry
    {
        [JsonProperty("battleroyale")]
        public SubGameInfo BattleRoyale { get; set; }
        
        [JsonProperty("creative")]
        public SubGameInfo Creative { get; set; }
        
        [JsonProperty("savetheworld")]
        public SubGameInfo SaveTheWorld { get; set; }
    }

    public class SubGameInfo
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
        
        [JsonProperty("subgame")]
        public string SubGame { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("standardMessageLine1")]
        public string StandardMessageLine1 { get; set; }
        
        [JsonProperty("standardMessageLine2")]
        public string StandardMessageLine2 { get; set; }
        
        [JsonProperty("specialMessage")]
        public string SpecialMessage { get; set; }
        
        [JsonProperty("color")]
        public string Color { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}