using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite
{
    public class AccountPrivacy
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("optOutOfPublicLeaderboards")]
        public bool OptOutOfPublicLeaderboards { get; set; }
    }
}