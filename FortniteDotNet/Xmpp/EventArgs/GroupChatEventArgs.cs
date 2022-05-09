using System.Linq;
using System.Xml;
using FortniteDotNet.Models.PartyService;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class GroupChatEventArgs
    {
        public Member From { get; }
        public string Body { get; }
        
        internal GroupChatEventArgs(Party party, XmlElement documentElement)
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