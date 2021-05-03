using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class RepeatableDailiesCardAttributes : BaseAttributes
    {
        [JsonProperty("days_since_season_start_grant")]
        public int DaysSinceSeasonStartGrant { get; set; }
        
        [JsonProperty("reduced_xp_reward")]
        public double ReducedXpReward { get; set; }
        
        [JsonProperty("granter_quest_pack")]
        public string GranterQuestPack { get; set; }
        
        [JsonProperty("fill_count")]
        public int FillCount { get; set; }
        
        [JsonProperty("replaced_rested_xp_value_scalar_for_missed_days")]
        public int ReplacedRestedXpValueScalarForMissedDays { get; set; }
        
        [JsonProperty("card_season_num")]
        public int CardSeasonNum { get; set; }
        
        [JsonProperty("replaced_rested_xp_value")]
        public int ReplacedRestedXpValue { get; set; }
    }
}