using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Events
{
    public class EventWindow
    {
        [JsonProperty("eventWindowId")] 
        public string EventWindowId { get; set; }
        
        [JsonProperty("eventTemplateId")] 
        public string EventTemplateId { get; set; }
        
        [JsonProperty("countdownBeginTime")] 
        public DateTime CountdownBeginTime { get; set; }
        
        [JsonProperty("beginTime")]
        public DateTime BeginTime { get; set; }
        
        [JsonProperty("endTime")] 
        public DateTime EndTime { get; set; }
        
        [JsonProperty("blackoutPeriods")]
        public object[] BlackoutPeriods { get; set; }
        
        [JsonProperty("round")] 
        public int Round { get; set; }
        
        [JsonProperty("payoutDelay")] 
        public int PayoutDelay { get; set; }
        
        [JsonProperty("isTBD")] 
        public bool IsTBD { get; set; }
        
        [JsonProperty("canLiveSpectate")]
        public bool CanLiveSpectate { get; set; }
        
        [JsonProperty("scoreLocations")]
        public List<ScoreLocation> ScoreLocations { get; set; }
        
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        
        [JsonProperty("requireAllTokens")]
        public List<string> RequireAllTokens { get; set; }
        
        [JsonProperty("requireAnyTokens")]
        public List<string> RequireAnyTokens { get; set; }
        
        [JsonProperty("requireNoneTokensCaller")]
        public List<string> RequireNoneTokensCaller { get; set; }
        
        [JsonProperty("requireAllTokensCaller")]
        public List<string> RequireAllTokensCaller { get; set; }
        
        [JsonProperty("requireAnyTokensCaller")]
        public List<string> RequireAnyTokensCaller { get; set; }
        
        [JsonProperty("additionalRequirements")]
        public List<string> AdditionalRequirements { get; set; }
        
        [JsonProperty("teammateEligibility")]
        public string TeammateEligibility { get; set; }
        
        [JsonProperty("metadata")]
        public object Metadata { get; set; }
    }

    public class ScoreLocation
    {
        [JsonProperty("scoreMode")]
        public string ScoreMode { get; set; }
        
        [JsonProperty("leaderboardId")]
        public string LeaderboardId { get; set; }
        
        [JsonProperty("scoreId")]
        public string ScoreId { get; set; }
        
        [JsonProperty("useIndividualScores")]
        public bool UseIndividualScores { get; set; }
    }
}