using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class UpdateQuestClientObjectives
    {
        [JsonProperty("advance")]
        public List<Advance> Advance { get; set; }
    }
    
    public class Advance
    {
        [JsonProperty("statName")]
        public string StatName { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("timestampOffset")]
        public int TimestampOffset { get; set; }
    }
}