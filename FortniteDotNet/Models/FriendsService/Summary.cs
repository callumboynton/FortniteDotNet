using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService
{
    public class Summary
    {
        [JsonProperty("friends")]
        public List<AcceptedFriend> Friends { get; set; }

        [JsonProperty("incoming")]
        public List<Friend> Incoming { get; set; }

        [JsonProperty("outgoing")]
        public List<Friend> Outgoing { get; set; }

        [JsonProperty("suggested")]
        public List<SuggestedFriend> Suggested { get; set; }

        [JsonProperty("blocklist")]
        public List<object> Blocklist { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("limitsReached")]
        public Dictionary<string, bool> LimitsReached { get; set; }
    }
}