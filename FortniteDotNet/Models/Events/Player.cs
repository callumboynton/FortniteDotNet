using Newtonsoft.Json;

namespace FortniteDotNet.Models.Events
{
    public class Player
    {
        [JsonProperty("gameId")] 
        public string GameId { get; set; }

        [JsonProperty("accountId")] 
        public string AccountId { get; set; }

        [JsonProperty("tokens")] 
        public object[] Tokens { get; set; }

        [JsonProperty("teams")]
        public object Teams { get; set; }
        
        [JsonProperty("pendingPayouts")]
        public object[] PendingPayouts { get; set; }
        
        [JsonProperty("pendingPenalties")]
        public object PendingPenalties { get; set; }
        
        [JsonProperty("persistentScores")]
        public object PersistentScores { get; set; }
        
        [JsonProperty("groupIdentity")]
        public object GroupIdentity { get; set; }
    }
}