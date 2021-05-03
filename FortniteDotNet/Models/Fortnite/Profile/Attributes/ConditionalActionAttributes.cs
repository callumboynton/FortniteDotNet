using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class ConditionalActionAttributes
    {
        [JsonProperty("alt_profile_types")] 
        public List<string> AltProfileTypes { get; set; }

        [JsonProperty("_private")]
        public bool Private { get; set; }

        [JsonProperty("devName")] 
        public string DevName { get; set; }

        [JsonProperty("conditions")] 
        public Conditions Conditions { get; set; }
    }

    public class Conditions
    {
        [JsonProperty("event")] 
        public Event Event { get; set; }
    }

    public class Event
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }
        
        [JsonProperty("eventName")]
        public string EventName { get; set; }
        
        [JsonProperty("eventStart")]
        public DateTime EventStart { get; set; }
        
        [JsonProperty("eventEnd")]
        public DateTime EventEnd { get; set; }
        
        [JsonProperty("startActions")]
        public StartActions StartActions { get; set; }
        
        [JsonProperty("endActions")]
        public EndActions EndActions { get; set; }
        
        [JsonProperty("metaData")]
        public object MetaData { get; set; }
    }

    public class StartActions
    {
        [JsonProperty("hasRun")]
        public bool HasRun { get; set; }
        
        [JsonProperty("conversions")]
        public object[] Conversions { get; set; }
        
        [JsonProperty("itemsToGrant")]
        public List<Item> ItemsToGrant { get; set; }
        
        [JsonProperty("questsToUnpause")]
        public object[] questsToUnpause { get; set; }
    }

    public class EndActions
    {
        [JsonProperty("hasRun")]
        public bool HasRun { get; set; }
        
        [JsonProperty("conversions")]
        public object[] Conversions { get; set; }
        
        [JsonProperty("itemsToRemove")]
        public List<Item> ItemsToRemove { get; set; }
        
        [JsonProperty("questsToPause")]
        public object[] QuestsToPause { get; set; }
    }
    
    public class Item
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }
        
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
} 