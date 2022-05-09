using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        private async Task SendAuth()
        {
            var auth = $"\u0000{AuthSession.AccountId}\u0000{AuthSession.AccessToken}".ToBase64();
        
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("auth", "urn:ietf:params:xml:ns:xmpp-sasl");
            {
                writer.WriteAttributeString("mechanism", "PLAIN");
            }
            writer.WriteString(auth);
            writer.WriteEndElement();
            writer.Flush();
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}