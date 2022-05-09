using System.Xml;
using FortniteDotNet.Xmpp.EventArgs;
using FortniteDotNet.Xmpp.Payloads;
using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp
{
    public partial class XmppClient
    {
        private void HandlePresence(XmlElement docElement)
        {
            var statusText = docElement.GetElementsByTagName("status")[0]?.InnerText;
            if (statusText == null)
                return;
            
            var presence = JsonConvert.DeserializeObject<Presence>(statusText);

            var show = docElement.GetElementsByTagName("show")[0];
            if (show != null)
            {
                presence.Availability = show.InnerText switch
                {
                    "away" => Availability.Away,
                    "xa" => Availability.ExtendedAway,
                    _ => presence.Availability
                };
            }

            var type = docElement.GetElementsByTagName("type")[0];
            if (type is {InnerText: "unavailable"})
                presence.Availability = Availability.Offline;

            onPresenceReceived(new PresenceEventArgs(docElement.GetAttribute("to"), docElement.GetAttribute("from"), presence, type?.InnerText));
        }
    }
}