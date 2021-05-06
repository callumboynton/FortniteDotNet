using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Calendar
{
    public class ChannelState
    {
        [JsonProperty("validFrom")]
        public DateTime ValidFrom { get; set; }

        [JsonProperty("activeEvents")]
        public List<ChannelEvent> ActiveEvents { get; set; }

        [JsonProperty("state")]
        public JObject State { get; set; }
    }
}