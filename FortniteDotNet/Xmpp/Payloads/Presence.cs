using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.PartyService;

namespace FortniteDotNet.Xmpp.Payloads
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

        [JsonProperty("ProductName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("Properties")]
        public Dictionary<string, object> Properties { get; set; }
        
        [JsonIgnore]
        public Availability Availability { get; set; }
        
        public Presence()
        {
            Status = "";
            IsPlaying = false;
            IsJoinable = false;
            HasVoiceSupport = false;
            SessionId = "";
            Properties = new Dictionary<string, object>();

            Availability = Availability.Online;
        }
        
        public Presence(Party party, Dictionary<string, object> properties)
        {
            Status = $"Battle Royale Lobby - {party.Members.Count} / {party.Config["max_size"]}";
            IsPlaying = false;
            IsJoinable = false;
            HasVoiceSupport = false;
            SessionId = "";
            Properties = properties;

            Availability = Availability.Online;
        }
    }

    public enum Availability
    {
        Online,
        Away,
        ExtendedAway,
        Offline
    }
}