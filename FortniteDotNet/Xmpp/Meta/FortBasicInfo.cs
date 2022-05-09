using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class FortBasicInfo
    {
        [JsonProperty("homeBaseRating")] 
        public int HomeBaseRating => 1;
    }
}