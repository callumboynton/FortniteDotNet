using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Payloads
{
    public class FriendRemoval
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}