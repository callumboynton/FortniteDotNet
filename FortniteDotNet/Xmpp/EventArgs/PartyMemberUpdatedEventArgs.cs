using System.Collections.Generic;
using FortniteDotNet.Models.PartyService;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class PartyMemberUpdatedEventArgs
    {
        public Member Member { get; set; }
        
        public Dictionary<string, object> UpdatedMeta { get; set; }
        public List<string> DeletedMeta { get; set; }
        
        internal PartyMemberUpdatedEventArgs(Member member, Dictionary<string, object> updatedMeta, List<string> deletedMeta)
        {
            Member = member;
            UpdatedMeta = updatedMeta;
            DeletedMeta = deletedMeta;
        }
    }
}