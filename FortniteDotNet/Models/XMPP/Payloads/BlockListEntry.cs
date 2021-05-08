using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Payloads
{
    public class BlockListEntry
    {
        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }
        
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }
}