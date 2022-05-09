using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class FrontendEmote
    {
        [JsonProperty("FrontendEmote")]
        public FrontendEmoteData Data { get; set; }

        public FrontendEmote(string eid = null, bool isEmoji = false, int section = -1)
        {
            Data = new FrontendEmoteData(eid, isEmoji, section);
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

        public FrontendEmoteData(string eid = null, bool isEmoji = false, int section = -1)
        {
            EmoteItemDefinition = eid == null ? "None" : 
                isEmoji ? $"/Game/Athena/Items/Cosmetics/Dances/Emoji/{eid}.{eid}" : 
                $"/Game/Athena/Items/Cosmetics/Dances/{eid}.{eid}";
            EmoteSection = section;
        }
    }
}