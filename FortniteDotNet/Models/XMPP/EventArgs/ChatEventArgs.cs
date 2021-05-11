using System.Xml;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class ChatEventArgs
    {
        public string From { get; init; }
        public string Body { get; init; }

        public ChatEventArgs(XmlElement documentElement)
        {
            From = documentElement.GetAttribute("from");
            Body = documentElement.InnerText;
        }
    }
}