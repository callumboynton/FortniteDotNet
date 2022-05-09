using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class PartyUpdate
    {
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("meta")]
        public UpdateMeta Meta { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        public PartyUpdate(Party party, Dictionary<string, object> updated, List<string> deleted = null)
        {
            Config = new Dictionary<string, object>
            {
                {"join_confirmation", party.Config["join_confirmation"]},
                {"joinability", party.Config["joinability"]},
                {"max_size", party.Config["max_size"]}
            };
            Meta = new UpdateMeta
            {
                Delete = deleted ?? new List<string>(),
                Update = updated
            };
            Revision = party.Revision;
        }
    }

    public class UpdateMeta
    {
        [JsonProperty("update")]
        public Dictionary<string, object> Update { get; set; }
        
        [JsonProperty("delete")]
        public List<string> Delete { get; set; }
    }
}