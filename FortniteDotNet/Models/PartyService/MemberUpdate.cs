using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class MemberUpdate
    {
        [JsonProperty("update")]
        public Dictionary<string, object> Update { get; set; }
        
        [JsonProperty("delete")]
        public List<string> Delete { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        public MemberUpdate(Member member, Dictionary<string, object> updated = null, List<string> deleted = null)
        {
            Update = updated ?? Member.SchemaMeta;
            Delete = deleted ?? new List<string>();
            
            Revision = member.Revision;
        }
    }
}