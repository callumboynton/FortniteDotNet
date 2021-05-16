using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class RawSquadAssignments
    {
        [JsonProperty("RawSquadAssignments")] 
        public List<RawSquadAssignment> Data { get; set; }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);

        public RawSquadAssignments(List<RawSquadAssignment> squadAssignments)
        {
            Data = squadAssignments;
        }
    }

    public class RawSquadAssignment
    {
        [JsonProperty("memberId")]
        public string MemberId { get; set; }
        
        [JsonProperty("absoluteMemberIdx")]
        public int AbsoluteMemberIndex { get; set; }

        public RawSquadAssignment(string memberId, int index = 0)
        {
            MemberId = memberId;
            AbsoluteMemberIndex = index;
        }
    }
}