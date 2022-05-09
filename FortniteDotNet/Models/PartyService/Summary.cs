using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class Summary
    {
        [JsonProperty("current")]
        public List<Party> Current { get; set; }
        
        [JsonProperty("pending")]
        public List<object> Pending { get; set; }
        
        [JsonProperty("invites")]
        public List<Invite> Invites { get; set; }
        
        [JsonProperty("pings")]
        public List<Ping> Pings { get; set; }
    }
}