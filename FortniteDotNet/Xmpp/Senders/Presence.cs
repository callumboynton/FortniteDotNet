using System;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Xmpp.Payloads;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        public async Task SendPresence(Presence presence, string type = null, string show = null)
        {
            LastPresence = presence;
            
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("presence");

            LastPresence.Availability = Availability.Online;

            if (type != null)
            {
                if (type == "unavailable")
                    LastPresence.Availability = Availability.Offline;
                
                writer.WriteStartElement("type");
                {
                    writer.WriteString(type);
                }
                writer.WriteEndElement();
            }
            if (show != null)
            {                
                LastPresence.Availability = show switch
                {
                    "away" => Availability.Away,
                    "xa" => Availability.ExtendedAway,
                    _ => presence.Availability
                };
                
                writer.WriteStartElement("show");
                {
                    writer.WriteString(show);
                }
                writer.WriteEndElement();
            }
            
            writer.WriteStartElement("status");
            {
                writer.WriteString(JsonConvert.SerializeObject(presence));
            }
            writer.WriteEndElement();
            writer.WriteStartElement("delay", "urn:xmpp:delay");
            {
                writer.WriteAttributeString("stamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            
            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
        
        public async Task JoinPartyChat()
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("presence");
            writer.WriteAttributeString("to", $"Party-{CurrentParty.Id}@muc.prod.ol.epicgames.com/{AuthSession.DisplayName}:{AuthSession.AccountId}:{Resource}");
            writer.WriteStartElement("x", "http://jabber.org/protocol/muc");
            {
                writer.WriteStartElement("history");
                {
                    writer.WriteAttributeString("maxstanzas", "50");
                }
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();

            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
        
        public async Task LeavePartyChat()
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, _writerSettings);

            writer.WriteStartElement("presence");
            writer.WriteAttributeString("to", $"Party-{CurrentParty.Id}@muc.prod.ol.epicgames.com/{AuthSession.DisplayName}:{AuthSession.AccountId}:{Resource}");
            writer.WriteAttributeString("type", "unavailable");
            writer.WriteEndElement();
            writer.Flush();

            await SendAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}
