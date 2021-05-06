using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite
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
        public List<JObject> ProfileChanges { get; set; }
        
        [JsonProperty("profileCommandRevision")]
        public int ProfileCommandRevision { get; set; }
        
        [JsonProperty("serverTime")]
        public DateTime ServerTime { get; set; }
        
        [JsonProperty("responseVersion")]
        public int ResponseVersion { get; set; }
        
        [JsonProperty("multiUpdate")]
        public List<JObject> MultiUpdate { get; set; }
    }
}