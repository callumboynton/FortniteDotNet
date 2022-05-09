using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class AccoladeAttributes
    {
        [JsonProperty("earned_count")]
        public int EarnedCount { get; set; }
        
        [JsonProperty("last_earned_day")]
        public int LastEarnedDay { get; set; }
    }
}