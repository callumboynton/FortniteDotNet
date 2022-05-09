using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class Ping
    {
        [JsonProperty("sent_by")]
        public string SentBy { get; set; }
        
        [JsonProperty("sent_to")]
        public string SentTo { get; set; }
        
        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }
        
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
        
        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }
    }
}