using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService.Legacy
{
    public class Blocklist
    {
        [JsonProperty("blockedUsers")]
        public List<string> BlockedUsers { get; set; }
    }
}