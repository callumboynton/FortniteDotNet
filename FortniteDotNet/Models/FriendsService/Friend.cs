using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService
{
    public class BaseFriend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }

    public class SuggestedFriend : BaseFriend
    {
        [JsonProperty("mutual")]
        public int Mutual { get; set; }
        
        [JsonProperty("connections")]
        public Dictionary<string, Connection> Connections { get; set; }
    }

    public class Friend : BaseFriend
    {
        [JsonProperty("mutual")]
        public int Mutual { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class AcceptedFriend : Friend
    {
        [JsonProperty("groups")]
        public List<object> Groups { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }

    public class RecentPlayer
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("lastPlayed")]
        public DateTime LastPlayed { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("cohortId")]
        public string CohortId { get; set; }
    }
}