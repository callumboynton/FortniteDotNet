using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Payloads.Fortnite
{
    public class MarkItemSeen
    {
        [JsonProperty("itemIds")]
        public List<string> ItemIds { get; set; }
    }
}