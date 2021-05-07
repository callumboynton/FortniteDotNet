using System;
using System.Xml;

namespace FortniteDotNet.Models.XMPP
{
    public class MessageEventArgs : EventArgs
    {
        public string MessageType { get; init; }
        public XmlDocument Document { get; init; }
    }
}