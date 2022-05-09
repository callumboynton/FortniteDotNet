using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class EventPurchaseTrackerAttributes
    {
        [JsonProperty("event_purchases")]
        public object EventPurchases { get; set; }
        
        [JsonProperty("_private")]
        public bool Private { get; set; }
        
        [JsonProperty("devName")]
        public string DevName { get; set; }
        
        [JsonProperty("event_instance_id")]
        public string EventInstanceId { get; set; }
    }
}