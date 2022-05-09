using FortniteDotNet.Xmpp.Payloads;

namespace FortniteDotNet.Xmpp.EventArgs
{
    public class PresenceEventArgs
    {
        public string To { get; }
        public string From { get; }
        public Presence Status { get; }
        public string Type { get; }

        internal PresenceEventArgs(string to, string from, Presence status, string type)
        {
            To = to;
            From = from;
            Status = status;
            Type = type;
        }
    }
}