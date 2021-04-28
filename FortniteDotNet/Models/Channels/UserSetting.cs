using Newtonsoft.Json;

namespace FortniteDotNet.Models.Channels
{
    public class UserSetting
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}