using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendOpen()
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("open", "urn:ietf:params:xml:ns:xmpp-framing");
            {
                writer.WriteAttributeString("to", "prod.ol.epicgames.com");
                writer.WriteAttributeString("version", "1.0");
            }
            await writer.WriteEndElementAsync().ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}