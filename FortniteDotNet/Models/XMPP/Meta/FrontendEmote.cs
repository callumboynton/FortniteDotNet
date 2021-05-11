using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class FrontendEmote
    {
        [JsonProperty("FrontendEmote")]
        public FrontendEmoteData Data { get; set; }

        public FrontendEmote(string eid = null, int section = -1)
        {
            Data = new FrontendEmoteData(eid, section);
        }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class FrontendEmoteData
    {
        [JsonProperty("emoteItemDef")] 
        public string EmoteItemDefinition { get; set; }

        [JsonProperty("emoteEKey")] 
        public string EmoteEKey => "";

        [JsonProperty("emoteSection")] 
        public int EmoteSection { get; set; }

        public FrontendEmoteData(string eid = null, int section = -1)
        {
            EmoteItemDefinition = eid == null ? "None" : $"/Game/Athena/Items/Cosmetics/Dances/{eid}.{eid}";
            EmoteSection = section;
        }
    }
}