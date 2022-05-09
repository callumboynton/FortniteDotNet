using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class AssistedChallengeInfo
    {
        [JsonProperty("AssistedChallengeInfo")]
        public AssistedChallengeInfoData Data => new AssistedChallengeInfoData();
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class AssistedChallengeInfoData
    {
        [JsonProperty("questItemDef")] 
        public string QuestItemDefinition => "None";
        
        [JsonProperty("objectivesCompleted")] 
        public int ObjectivesCompleted => 0;
    }
}