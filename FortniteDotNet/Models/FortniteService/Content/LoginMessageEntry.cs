using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class LoginMessageEntry : BasePagesEntry
    {
        [JsonProperty("loginmessage")]
        public Message LoginMessage { get; set; }
    }
}