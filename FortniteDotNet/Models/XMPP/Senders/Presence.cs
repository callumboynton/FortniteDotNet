using System;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendPresence(Presence presence)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("presence");
            writer.WriteAttributeString("type", "available");
            writer.WriteStartElement("status");
            {
                await writer.WriteStringAsync(JsonConvert.SerializeObject(presence));
            }
            await writer.WriteEndElementAsync();
            writer.WriteStartElement("delay", "urn:xmpp:delay");
            {
                writer.WriteAttributeString("stamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            }
            await writer.WriteEndElementAsync();
            await writer.WriteEndElementAsync();
            await writer.FlushAsync();
            
            await SendMessage(builder.ToString());
        }
    }
}