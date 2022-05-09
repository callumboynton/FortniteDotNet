using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Calendar
{
    public class ChannelEvent
    {
        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("activeUntil")]
        public DateTime ActiveUntil { get; set; }

        [JsonProperty("activeSince")]
        public DateTime ActiveSince { get; set; }
    }
}