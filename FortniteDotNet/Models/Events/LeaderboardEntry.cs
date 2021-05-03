using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Events
{
    public class LeaderboardEntry
    {
        [JsonProperty("gameId")]
        public string GameId { get; set; }
        
        [JsonProperty("eventId")]
        public string EventId { get; set; }
        
        [JsonProperty("eventWindowId")]
        public string EventWindowId { get; set; }
        
        [JsonProperty("teamAccountIds")]
        public List<string> TeamAccountIds { get; set; }
        
        [JsonProperty("liveSessionId")]
        public string LiveSessionId { get; set; }
        
        [JsonProperty("pointsEarned")]
        public int PointsEarned { get; set; }
        
        [JsonProperty("score")]
        public long Score { get; set; }
        
        [JsonProperty("rank")]
        public int Rank { get; set; }
        
        [JsonProperty("percentile")]
        public double Percentile { get; set; }
        
        [JsonProperty("pointBreakdown")]
        public Dictionary<string, PointBreakdown> PointBreakdown { get; set; }
        
        [JsonProperty("sessionHistory")]
        public List<EventSession> SessionHistory { get; set; }
        
        [JsonProperty("tokens")]
        public object[] Tokens { get; set; }
        
        [JsonProperty("teamId")]
        public string TeamId { get; set; }
    }

    public class PointBreakdown
    {
        [JsonProperty("timesAchieved")]
        public int TimesAchieved { get; set; }
        
        [JsonProperty("pointsEarned")]
        public int PointsEarned { get; set; }
    }

    public class EventSession
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("endTime")] 
        public DateTime EndTime { get; set; }
    }

    public class TrackedStats
    {
        [JsonProperty("PLACEMENT_STAT_INDEX")]
        public int Placement { get; set; }
        
        [JsonProperty("TIME_ALIVE_STAT")]
        public int SecondsAlive { get; set; }
        
        [JsonProperty("TEAM_ELIMS_STAT_INDEX")]
        public int TeamEliminations { get; set; }
        
        [JsonProperty("MATCH_PLAYED_STAT")]
        public int MatchesPlayed { get; set; }
        
        [JsonProperty("PLACEMENT_TIEBREAKER_STAT")]
        public int PlacementTiebreaker { get; set; }
        
        [JsonProperty("VICTORY_ROYALE_STAT")]
        public int VictoryRoyales { get; set; }
    }
}