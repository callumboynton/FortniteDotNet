using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class MessageEntry
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }
    
    public class Message
    { 
        [JsonProperty("_type")] 
        public string Type { get; set; }

        [JsonProperty("hidden")] 
        public bool Hidden { get; set; }

        [JsonProperty("spotlight")] 
        public bool Spotlight { get; set; }

        [JsonProperty("messagetype")] 
        public string MessageType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}