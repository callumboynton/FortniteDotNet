using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class Invite
    {
        [JsonProperty("party_id")]
        public string PartyId { get; set; }

        [JsonProperty("sent_to")]
        public string SentTo { get; set; }

        [JsonProperty("sent_by")]
        public string SentBy { get; set; }

        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }
    }
}