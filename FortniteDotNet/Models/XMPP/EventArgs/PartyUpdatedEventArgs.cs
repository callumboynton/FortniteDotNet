using System.Collections.Generic;
using FortniteDotNet.Models.Party;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class PartyUpdatedEventArgs
    {
        public PartyInfo Party { get; init; }
        
        public Dictionary<string, object> UpdatedMeta { get; init; }
        public List<string> DeletedMeta { get; init; }

        public PartyUpdatedEventArgs(PartyInfo party, Dictionary<string, object> updatedMeta, List<string> deletedMeta)
        {
            Party = party;
            UpdatedMeta = updatedMeta;
            DeletedMeta = deletedMeta;
        }
    }
}