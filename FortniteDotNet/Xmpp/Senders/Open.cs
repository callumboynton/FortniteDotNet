using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        private async Task SendOpen()
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("open", "urn:ietf:params:xml:ns:xmpp-framing");
            {
                writer.WriteAttributeString("to", "prod.ol.epicgames.com");
                writer.WriteAttributeString("version", "1.0");
            }
            writer.WriteEndElement();
            writer.Flush();
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}