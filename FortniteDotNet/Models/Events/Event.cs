using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Events
{
    public class Event
    {
        [JsonProperty("gameId")] 
        public string GameId { get; set; }
        
        [JsonProperty("eventId")] 
        public string EventId { get; set; }
        
        [JsonProperty("regions")] 
        public List<string> Regions { get; set; }
        
        [JsonProperty("regionMappings")]
        public Dictionary<string ,string> RegionMappings { get; set; }
        
        [JsonProperty("platforms")] 
        public List<string> Platforms { get; set; }
        
        [JsonProperty("platformMappings")]
        public Dictionary<string ,string> PlatformMappings { get; set; }
        
        [JsonProperty("displayDataId")] 
        public string DisplayDataId { get; set; }
        
        [JsonProperty("eventGroup")] 
        public string EventGroup { get; set; }
        
        [JsonProperty("announcementTime")] 
        public DateTime AnnouncementTime { get; set; }
        
        [JsonProperty("appId")]
        public string AppId { get; set; }
        
        [JsonProperty("environment")]
        public string Environment { get; set; }
        
        [JsonProperty("metadata")]
        public object Metadata { get; set; }
        
        [JsonProperty("eventWindows")]
        public List<EventWindow> EventWindows { get; set; }
        
        [JsonProperty("beginTime")]
        public DateTime BeginTime { get; set; }
        
        [JsonProperty("endTime")] 
        public DateTime EndTime { get; set; }
    }
}