using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.Channels
{
    public class UpdateUserSetting
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}