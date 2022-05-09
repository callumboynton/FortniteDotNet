using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Payloads
{
    public class BlockListEntry
    {
        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }
        
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }
}