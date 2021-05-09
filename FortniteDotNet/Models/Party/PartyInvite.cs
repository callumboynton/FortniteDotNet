using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyInvite
    {
        [JsonProperty("party_id")]
        public string PartyId { get; set; }

        [JsonProperty("sent_by")]
        public string SentBy { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("sent_to")]
        public string SentTo { get; set; }

        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}