using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Party
{
    public class PartyUpdate
    {
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("meta")]
        public PartyUpdateMeta Meta { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
        
        public PartyUpdate(PartyInfo partyInfo, Dictionary<string, object> updated, List<string> deleted)
        {
            Config = new()
            {
                {"join_confirmation", partyInfo.Config["join_confirmation"]},
                {"joinability", partyInfo.Config["joinability"]},
                {"max_size", partyInfo.Config["max_size"]}
            };
            Meta = new PartyUpdateMeta
            {
                Delete = deleted ?? new(),
                Update = updated
            };
            Revision = partyInfo.Revision;
        }
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
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);

        public PartyMemberUpdate(PartyMember partyMember, Dictionary<string, object> updated, List<string> deleted)
        {
            Delete = deleted ?? new();
            Update = updated ?? PartyMember.SchemaMeta;
            Revision = partyMember.Revision;
        }
    }
}