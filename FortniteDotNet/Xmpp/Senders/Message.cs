using System;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        public async Task SendMessage(string body, string to, bool isGroupChat = false)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("message");
            writer.WriteAttributeString("to", to);

            if (isGroupChat)
            {
                writer.WriteAttributeString("id", Guid.NewGuid().ToString().Replace("-", "").ToUpper());
                writer.WriteAttributeString("type", "groupchat");
            }
            else
            {
                writer.WriteAttributeString("type", "chat");
            }
            
            writer.WriteStartElement("body");
            {
                writer.WriteString(body);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}