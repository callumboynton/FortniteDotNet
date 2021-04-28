using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Events
{
    public class EventData
    {
        [JsonProperty("player")]
        public Player Player { get; set; }
        
        [JsonProperty("events")]
        public List<Event> Events { get; set; }
        
        [JsonProperty("templates")]
        public List<Template> Templates { get; set; }
        
        [JsonProperty("scores")]
        public object[] Scores { get; set; }
    }
}