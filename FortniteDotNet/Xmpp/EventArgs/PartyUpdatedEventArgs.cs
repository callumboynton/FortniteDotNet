using System.Collections.Generic;
using FortniteDotNet.Models.PartyService;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class PartyUpdatedEventArgs
    {
        public Party Party { get; }
        
        public Dictionary<string, object> UpdatedMeta { get; }
        public List<string> DeletedMeta { get; }

        internal PartyUpdatedEventArgs(Party party, Dictionary<string, object> updatedMeta, List<string> deletedMeta)
        {
            Party = party;
            UpdatedMeta = updatedMeta;
            DeletedMeta = deletedMeta;
        }
    }
}