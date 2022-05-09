using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class IssueFriendCode
    {
        [JsonProperty("codeTokenType")]
        public string CodeTokenType { get; set; }
    }
}