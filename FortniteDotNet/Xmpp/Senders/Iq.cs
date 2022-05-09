using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        private async Task SendIq(string id)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

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
                                writer.WriteString(Resource);
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
            
                    await SendAsync(builder.ToString()).ConfigureAwait(false);
                    break;
                }
                case "_xmpp_session1":
                {
                    writer.WriteStartElement("iq");
                    {
                        writer.WriteAttributeString("id", id);
                        writer.WriteAttributeString("type", "set");
                        writer.WriteStartElement("session", "urn:ietf:params:xml:ns:xmpp-session");
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
            
                    await SendAsync(builder.ToString()).ConfigureAwait(false);
                    break;
                }
            }
        }
    }
}