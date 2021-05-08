using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Payloads
{
    public class Friend
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
}