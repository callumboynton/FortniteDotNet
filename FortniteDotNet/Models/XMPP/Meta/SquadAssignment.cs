using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class SquadAssignment
    {
        [JsonProperty("memberId")]
        public string MemberId { get; set; }
        
        [JsonProperty("absoluteMemberIdx")]
        public int AbsoluteMemberIndex { get; set; }

        public SquadAssignment(string memberId, int index = 0)
        {
            MemberId = memberId;
            AbsoluteMemberIndex = index;
        }
    }
}