using System.Xml;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class MessageEventArgs
    {
        public string MessageType { get; init; }
        public XmlDocument Document { get; init; }
    }
}