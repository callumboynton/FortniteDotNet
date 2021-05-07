using System;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendMessage(string body, string to, bool isGroupChat = false)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("message");
            writer.WriteAttributeString("to", to);

            if (isGroupChat)
            {
                writer.WriteAttributeString("id", Guid.NewGuid().ToString().Replace("-", "").ToUpper());
                writer.WriteAttributeString("type", "groupchat");
            }
            else
                writer.WriteAttributeString("type", "chat");
            
            writer.WriteStartElement("body");
            {
                await writer.WriteStringAsync(body).ConfigureAwait(false);
            }
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}