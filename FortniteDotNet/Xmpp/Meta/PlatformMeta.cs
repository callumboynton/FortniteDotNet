using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class PlatformMeta
    {
        [JsonProperty("PlatformData")] 
        public PlatformData Data => new PlatformData();
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class PlatformData
    {
        [JsonProperty("platform")] 
        public Platform Platform => new Platform();

        [JsonProperty("uniqueId")] 
        public string UniqueId => "INVALID";

        [JsonProperty("sessionId")] 
        public string SessionId => "";
    }

    public class Platform
    {
        [JsonProperty("platformDescription")] 
        public PlatformDescription Description => new PlatformDescription();
    }

    public class PlatformDescription
    {
        [JsonProperty("name")] 
        public string Name => "WIN";
        
        [JsonProperty("platformType")] 
        public string PlatformType => "DESKTOP";
        
        [JsonProperty("onlineSubsystem")] 
        public string OnlineSubsystem => "None";
        
        [JsonProperty("sessionType")] 
        public string SessionType => "";
        
        [JsonProperty("externalAccountType")] 
        public string ExternalAccountType => "";
        
        [JsonProperty("crossplayPool")] 
        public string CrossplayPool => "DESKTOP";
    }
}