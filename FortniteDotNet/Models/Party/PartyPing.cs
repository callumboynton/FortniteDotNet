using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyPing
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
        public Dictionary<string, object> Meta { get; set; }
    }
}