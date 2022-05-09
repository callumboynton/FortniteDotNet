using System.Xml;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class MessageEventArgs
    {
        public string MessageType { get; }
        public XmlElement DocumentElement { get; }

        internal MessageEventArgs(XmlElement documentElement)
        {
            MessageType = documentElement.Name;
            DocumentElement = documentElement;
        }
    }
}