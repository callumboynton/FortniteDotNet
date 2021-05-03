using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.Fortnite.Profile;

namespace FortniteDotNet.Models.Fortnite.Mcp
{
    public class McpResponse
    {
        [JsonProperty("profileRevision")]
        public int ProfileRevision { get; set; }
        
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }
        
        [JsonProperty("profileChangesBaseRevision")]
        public int ProfileChangesBaseRevision { get; set; }
        
        [JsonProperty("profileChanges")]
        public List<ProfileChange> ProfileChanges { get; set; }
        
        [JsonProperty("profileCommandRevision")]
        public int ProfileCommandRevision { get; set; }
        
        [JsonProperty("serverTime")]
        public DateTime ServerTime { get; set; }
        
        [JsonProperty("responseVersion")]
        public int ResponseVersion { get; set; }
    }
}