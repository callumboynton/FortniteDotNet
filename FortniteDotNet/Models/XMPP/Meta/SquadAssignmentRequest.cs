using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class SquadAssignmentRequest
    {
        [JsonProperty("MemberSquadAssignmentRequest")]
        public SquadAssignmentRequestData Data => new SquadAssignmentRequestData();
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class SquadAssignmentRequestData
    {
        [JsonProperty("startingAbsoluteIdx")] 
        public int StartingAbsoluteIndex => -1;
        
        [JsonProperty("targetAbsoluteIdx")]
        public int TargetAbsoluteIdx => -1;
        
        [JsonProperty("swapTargetMemberId")]
        public string SwapTargetMemberId => "INVALID";
        
        [JsonProperty("version")]
        public int Version => 0;
    }
}