using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Events
{
    public class Leaderboard
    {
        [JsonProperty("gameId")]
        public string GameId { get; set; }
        
        [JsonProperty("eventId")]
        public string EventId { get; set; }
        
        [JsonProperty("eventWindowId")]
        public string EventWindowId { get; set; }
        
        [JsonProperty("page")]
        public int Page { get; set; }
        
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        
        [JsonProperty("updatedTime")]
        public DateTime UpdatedTime { get; set; }
        
        [JsonProperty("entries")]
        public List<LeaderboardEntry> Entries { get; set; }
        
        [JsonProperty("liveSessions")]
        public object LiveSessions { get; set; }
    }
}