using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendIq(string id)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            switch (id)
            {
                case "_xmpp_bind1":
                {
                    writer.WriteStartElement("iq");
                    {
                        writer.WriteAttributeString("id", id);
                        writer.WriteAttributeString("type", "set");
                        writer.WriteStartElement("bind", "urn:ietf:params:xml:ns:xmpp-bind");
                        {
                            writer.WriteStartElement("resource");
                            {
                                await writer.WriteStringAsync(Resource);
                            }
                            await writer.WriteEndElementAsync();
                        }
                        await writer.WriteEndElementAsync();
                    }
                    await writer.WriteEndElementAsync();
                    await writer.FlushAsync();
            
                    await SendMessage(builder.ToString());
                    break;
                }
                case "_xmpp_session1":
                {
                    writer.WriteStartElement("iq");
                    {
                        writer.WriteAttributeString("id", id);
                        writer.WriteAttributeString("type", "set");
                        writer.WriteStartElement("session", "urn:ietf:params:xml:ns:xmpp-session");
                        await writer.WriteEndElementAsync();
                    }
                    await writer.WriteEndElementAsync();
                    await writer.FlushAsync();
            
                    await SendMessage(builder.ToString());
                    break;
                }
            }
        }
    }
}