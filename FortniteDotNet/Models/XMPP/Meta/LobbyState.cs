using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class LobbyState
    {
        [JsonProperty("LobbyState")] 
        public LobbyStateData Data => new LobbyStateData();
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class LobbyStateData
    {
        [JsonProperty("inGameReadyCheckStatus")]
        public string InGameReadyCheckStatus => "None";

        [JsonProperty("gameReadiness")] 
        public string GameReadiness => "NotReady";

        [JsonProperty("readyInputType")] 
        public string ReadyInputType => "Count";

        [JsonProperty("currentInputType")] 
        public string CurrentInputType => "MouseAndKeyboard";

        [JsonProperty("hiddenMatchmakingDelayMax")]
        public int HiddenMatchmakingDelayMax => 0;

        [JsonProperty("hasPreloadedAthena")] 
        public bool HasPreloadedAthena => false;
    }
}