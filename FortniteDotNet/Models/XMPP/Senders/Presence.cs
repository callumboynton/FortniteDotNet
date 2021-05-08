using System;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Models.XMPP.Payloads;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendPresence(Presence presence, bool isAvailable = true)
        {
            LastPresence = presence;
            
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("presence");
            writer.WriteAttributeString("type", isAvailable ? "available" : "unavailable");
            writer.WriteStartElement("status");
            {
                await writer.WriteStringAsync(JsonConvert.SerializeObject(presence)).ConfigureAwait(false);
            }
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            writer.WriteStartElement("delay", "urn:xmpp:delay");
            {
                writer.WriteAttributeString("stamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            }
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}