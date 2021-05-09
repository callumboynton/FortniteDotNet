using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyJoinRequest
    {
        [JsonProperty("users")]
        public List<JoinRequestUser> Users { get; set; }
    }

    public class JoinRequestUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("dn")]
        public string DisplayName { get; set; }

        [JsonProperty("plat")]
        public string Platform { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
}