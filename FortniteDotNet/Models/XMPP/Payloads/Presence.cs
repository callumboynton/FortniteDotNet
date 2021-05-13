using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.Party;

namespace FortniteDotNet.Models.XMPP.Payloads
{
    public class Presence
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("bIsPlaying")]
        public bool IsPlaying { get; set; }

        [JsonProperty("bIsJoinable")]
        public bool IsJoinable { get; set; }

        [JsonProperty("bHasVoiceSupport")]
        public bool HasVoiceSupport { get; set; }

        [JsonProperty("SessionId")]
        public string SessionId { get; set; }

        [JsonProperty("Properties")]
        public Dictionary<string, object> Properties { get; set; }

        public Presence()
        {
            Status = "";
            IsPlaying = false;
            IsJoinable = false;
            HasVoiceSupport = false;
            SessionId = "";
            Properties = new();
        }

        public Presence(PartyInfo partyInfo, Dictionary<string, object> properties)
        {
            Status = $"Battle Royale Lobby - {partyInfo.Members.Count} / {partyInfo.Config["max_size"]}";
            IsPlaying = false;
            IsJoinable = false;
            HasVoiceSupport = false;
            SessionId = "";
            Properties = properties;
        }
    }
}