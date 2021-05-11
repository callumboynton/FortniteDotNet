using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class PartyJoinInfo
    {
        [JsonProperty("sourceId")]
        public string SourceId { get; set; }
        
        [JsonProperty("sourceDisplayName")]
        public string SourceDisplayName { get; set; }

        [JsonProperty("sourcePlatform")] 
        public string SourcePlatform => "WIN";
        
        [JsonProperty("partyId")]
        public string PartyId { get; set; }

        [JsonProperty("partyTypeId")] 
        public int PartyTypeId => 286331153;

        [JsonProperty("key")] 
        public string Key => "k";

        [JsonProperty("appId")] 
        public string AppId => "Fortnite";

        [JsonProperty("buildId")] 
        public string BuildId => "1:3:";

        [JsonProperty("partyFlags")] 
        public int PartyFlags => -2024557306;

        [JsonProperty("notAcceptingReason")] 
        public int NotAcceptingReason => 0;
        
        [JsonProperty("pc")]
        public int PartyCount { get; set; }

        public PartyJoinInfo(string accountId, string displayName, string partyId, int memberCount)
        {
            SourceId = accountId;
            SourceDisplayName = displayName;
            PartyId = partyId;
            PartyCount = memberCount;
        }
    }
}