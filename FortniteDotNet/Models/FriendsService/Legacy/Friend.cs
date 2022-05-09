using System;
using FortniteDotNet.Enums.FriendsService;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService.Legacy
{
    public class Friend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("status")]
        public Status Status { get; set; }
        
        [JsonProperty("direction")]
        public Direction Direction { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}