using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        /// <summary>
        /// Sends the IQ.
        /// </summary>
        /// <param name="id">The IQ ID.</param>
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
                                await writer.WriteStringAsync(Resource).ConfigureAwait(false);
                            }
                            await writer.WriteEndElementAsync().ConfigureAwait(false);
                        }
                        await writer.WriteEndElementAsync().ConfigureAwait(false);
                    }
                    await writer.WriteEndElementAsync().ConfigureAwait(false);
                    await writer.FlushAsync().ConfigureAwait(false);
            
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
                        await writer.WriteEndElementAsync().ConfigureAwait(false);
                    }
                    await writer.WriteEndElementAsync().ConfigureAwait(false);
                    await writer.FlushAsync().ConfigureAwait(false);
            
                    await SendAsync(builder.ToString()).ConfigureAwait(false);
                    break;
                }
            }
        }
    }
}