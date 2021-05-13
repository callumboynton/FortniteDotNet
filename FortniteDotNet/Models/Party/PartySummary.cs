using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartySummary
    {
        [JsonProperty("current")]
        public List<PartyInfo> Current { get; set; }
        
        [JsonProperty("pending")]
        public object[] Pending { get; set; }
        
        [JsonProperty("invites")]
        public List<PartyInvite> Invites { get; set; }
        
        [JsonProperty("pings")]
        public List<PartyPing> Pings { get; set; }
    }
}