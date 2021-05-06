using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Calendar
{
    public class Timeline
    {
        [JsonProperty("channels")]
        public Dictionary<string, TimelineChannel> Channels { get; set; }

        [JsonProperty("currentTime")]
        public DateTime CurrentTime { get; set; }

        [JsonProperty("cacheIntervalMins")]
        public double CacheIntervalMinutes { get; set; }
    }
}