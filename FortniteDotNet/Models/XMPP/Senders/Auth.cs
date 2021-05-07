using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public async Task SendAuth()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"\u0000{AuthSession.AccountId}\u0000{AuthSession.AccessToken}"));
        
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("auth", "urn:ietf:params:xml:ns:xmpp-sasl");
            {
                writer.WriteAttributeString("mechanism", "PLAIN");
            }
            await writer.WriteStringAsync(auth);
            await writer.WriteEndElementAsync();
            await writer.FlushAsync();
            
            await SendMessage(builder.ToString());
        }
    }
}