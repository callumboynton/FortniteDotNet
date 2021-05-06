using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Calendar.States
{
    public class ClientEventsState
    {
        [JsonProperty("activeStorefronts")]
        public string[] ActiveStorefronts { get; set; }

        [JsonProperty("eventNamedWeights")]
        public Dictionary<string, double> EventNamedWeights { get; set; }
        
        [JsonProperty("activeEvents")]
        public List<ActiveEvent> ActiveEvents { get; set; }

        [JsonProperty("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonProperty("seasonTemplateId")]
        public string SeasonTemplateId { get; set; }

        [JsonProperty("matchXpBonusPoints")]
        public double MatchXpBonusPoints { get; set; }

        [JsonProperty("eventPunchCardTemplateId")]
        public string EventPunchCardTemplateId { get; set; }

        [JsonProperty("seasonBegin")]
        public DateTime SeasonBegin { get; set; }

        [JsonProperty("seasonEnd")]
        public DateTime SeasonEnd { get; set; }

        [JsonProperty("seasonDisplayedEnd")]
        public DateTime SeasonDisplayedEnd { get; set; }

        [JsonProperty("weeklyStoreEnd")]
        public DateTime WeeklyStoreEnd { get; set; }

        [JsonProperty("dailyStoreEnd")]
        public DateTime DailyStoreEnd { get; set; }

        [JsonProperty("stwDailyStoreEnd")]
        public DateTime StwDailyStoreEnd { get; set; }

        [JsonProperty("stwEventStoreEnd")]
        public DateTime StwEventStoreEnd { get; set; }

        [JsonProperty("stwWeeklyStoreEnd")]
        public DateTime StwWeeklyStoreEnd { get; set; }

        [JsonProperty("sectionStoreEnds")]
        public Dictionary<string, DateTime> SectionStoreEnds { get; set; }
        
        [JsonProperty("rmtPromotion")]
        public string RMTPromotion { get; set; }
    }

    public class ActiveEvent
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("devName")]
        public string DevName { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventStart")]
        public DateTime EventStart { get; set; }

        [JsonProperty("eventEnd")]
        public DateTime EventEnd { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }
    }
}