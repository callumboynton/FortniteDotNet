using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Events
{
    public class Template
    {
        [JsonProperty("gameId")] 
        public string GameId { get; set; }
        
        [JsonProperty("eventTemplateId")] 
        public string EventTemplateId { get; set; }
        
        [JsonProperty("playlistId")] 
        public string PlaylistId { get; set; }
        
        [JsonProperty("matchCap")] 
        public int MatchCap { get; set; }
        
        [JsonProperty("liveSessionAttributes")]
        public List<string> LiveSessionAttributes { get; set; }
        
        [JsonProperty("scoringRules")] 
        public List<ScoringRule> ScoringRules { get; set; }
        
        [JsonProperty("tiebreakerFormula")]
        public TiebreakerFormula TiebreakerFormula { get; set; }
        
        [JsonProperty("payoutTable")] 
        public List<PayoutTable> PayoutTable { get; set; }
    }

    public class ScoringRule
    {
        [JsonProperty("trackedStat")]
        public string TrackedStat { get; set; }
        
        [JsonProperty("matchRule")]
        public string MatchRule { get; set; }
        
        [JsonProperty("rewardTiers")]
        public List<RewardTier> RewardTiers { get; set; }
    }

    public class RewardTier
    {
        [JsonProperty("keyValue")]
        public int KeyValue { get; set; }
        
        [JsonProperty("pointsEarned")]
        public int PointsEarned { get; set; }
        
        [JsonProperty("multiplicative")]
        public bool Multiplicative { get; set; }
    }

    public class TiebreakerFormula
    {
        [JsonProperty("basePointsBits")]
        public int BasePointsBits { get; set; }
        
        [JsonProperty("components")]
        public List<Component> Components { get; set; }
    }

    public class Component
    {
        [JsonProperty("trackedStat")]
        public string TrackedStat { get; set; }
        
        [JsonProperty("bits")]
        public int Bits { get; set; }
        
        [JsonProperty("multiplier", NullValueHandling = NullValueHandling.Ignore)]
        public double Multiplier { get; set; }
        
        [JsonProperty("aggregation")]
        public string Aggregation { get; set; }
    }

    public class PayoutTable
    {
        [JsonProperty("scoreId")]
        public string ScoreId { get; set; }
        
        [JsonProperty("scoringType")]
        public string ScoringType { get; set; }
        
        [JsonProperty("ranks")]
        public List<Rank> Ranks { get; set; }
    }

    public class Rank
    {
        [JsonProperty("threshold")]
        public double Threshold { get; set; }
        
        [JsonProperty("payouts")]
        public List<Payout> Payouts { get; set; }
    }

    public class Payout
    {
        [JsonProperty("rewardType")]
        public string RewardType { get; set; }
        
        [JsonProperty("rewardMode")]
        public string RewardMode { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}