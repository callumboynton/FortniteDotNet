using System.Xml;
using System.Linq;
using FortniteDotNet.Models.Party;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class GroupChatEventArgs
    {
        public PartyMember From { get; set; }
        public string Body { get; set; }
        
        public GroupChatEventArgs(PartyInfo party, XmlElement documentElement)
        {
            if (party == null || party.Id.Split("@")[0].Replace("Party-", "") != party.Id)
                return;
            
            if (documentElement.InnerText == "Welcome! You created new Multi User Chat Room.")
                return;
            
            var id = documentElement.GetAttribute("from").Split("/")[1].Split(":")[1];
            var member = party.Members.FirstOrDefault(x => x.Id == id);
            if (member == null)
                return;
            
            From = member;
            Body = documentElement.InnerText;
        }
    }
}