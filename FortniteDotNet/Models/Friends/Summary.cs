using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Friends
{
    public class Summary
    {
        [JsonProperty("friends")]
        public List<Friend> Friends { get; set; }
        
        [JsonProperty("incoming")]
        public List<PendingFriend> Incoming { get; set; }
        
        [JsonProperty("outgoing")]
        public List<PendingFriend> Outgoing { get; set; }
        
        [JsonProperty("suggested")]
        public List<SuggestedFriend> Suggested { get; set; }
        
        [JsonProperty("blocklist")]
        public List<BlockedFriend> Blocklist { get; set; }
        
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
        
        [JsonProperty("limitsReached")]
        public LimitsReached LimitsReached { get; set; }
    }
}