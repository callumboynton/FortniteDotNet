using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyJoinInfo
    {
        [JsonProperty("connection")]
        public PartyMemberConnection Connection { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }
    }
}