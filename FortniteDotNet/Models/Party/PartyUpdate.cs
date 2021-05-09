using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyUpdate
    {
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("meta")]
        public PartyUpdateMeta Meta { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }
    }

    public class PartyUpdateMeta
    {
        [JsonProperty("delete")]
        public List<string> Delete { get; set; }

        [JsonProperty("update")]
        public Dictionary<string, object> Update { get; set; }
    }

    public class PartyMemberUpdate
    {
        [JsonProperty("delete")]
        public List<string> Delete { get; set; }

        [JsonProperty("update")]
        public Dictionary<string, object> Update { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }
    }
}