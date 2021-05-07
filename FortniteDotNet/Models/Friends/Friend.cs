using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Friends
{
    public class Friend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("groups")]
        public object[] Groups { get; set; }
        
        [JsonProperty("mutual")]
        public int Mutual { get; set; }
        
        [JsonProperty("alias")]
        public string Alias { get; set; }
        
        [JsonProperty("note")]
        public string Note { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class PendingFriend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class SuggestedFriend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("connections")]
        public Dictionary<string, object> Connections { get; set; }
        
        [JsonProperty("mutual")]
        public int Mutual { get; set; }
    }

    public class BlockedFriend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }

    public class Settings
    {
        [JsonProperty("acceptInvites")]
        public string AcceptInvites { get; set; }
    }

    public class EpicConnection
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("sortFactors")]
        public SortFactors SortFactors { get; set; }
    }

    public class SortFactors
    {
        [JsonProperty("x")]
        public int X { get; set; }
        
        [JsonProperty("y")]
        public int Y { get; set; }
        
        [JsonProperty("k")]
        public DateTime K { get; set; }
        
        [JsonProperty("l")]
        public DateTime L { get; set; }
    }

    public class LimitsReached
    {
        [JsonProperty("incoming")]
        public bool Incoming { get; set; }
        
        [JsonProperty("outgoing")]
        public bool Outgoing { get; set; }
        
        [JsonProperty("accepted")]
        public bool Accepted { get; set; }
    }
}