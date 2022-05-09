using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class BattleRoyaleNewsEntry : BasePagesEntry
    {
        [JsonProperty("news")]
        public BattleRoyaleNews News { get; set; }
    }

    public class BattleRoyaleNews
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
        
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("motds")]
        public List<object> MessagesOfTheDay { get; set; }
    }
}