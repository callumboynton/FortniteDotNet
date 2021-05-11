using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class FortBasicInfo
    {
        [JsonProperty("homeBaseRating")] 
        public int HomeBaseRating => 1;
    }
}