using System.Collections.Generic;
using FortniteDotNet.Models.Party;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class PartyMemberUpdatedEventArgs
    {
        public PartyMember Member { get; init; }
        
        public Dictionary<string, object> UpdatedMeta { get; init; }
        public List<string> DeletedMeta { get; init; }
        
        public PartyMemberUpdatedEventArgs(PartyMember member, Dictionary<string, object> updatedMeta, List<string> deletedMeta)
        {
            Member = member;
            UpdatedMeta = updatedMeta;
            DeletedMeta = deletedMeta;
        }
    }
}