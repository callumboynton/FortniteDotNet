using System.Xml;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class ChatEventArgs
    {
        public string From { get; }
        public string Body { get; }

        internal ChatEventArgs(XmlElement documentElement)
        {
            From = documentElement.GetAttribute("from");
            Body = documentElement.InnerText;
        }
    }
}