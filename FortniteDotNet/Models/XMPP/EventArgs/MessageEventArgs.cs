using System.Xml;

namespace FortniteDotNet.Models.XMPP.EventArgs
{
    public class MessageEventArgs : System.EventArgs
    {
        public string MessageType { get; init; }
        public XmlDocument Document { get; init; }
    }
}