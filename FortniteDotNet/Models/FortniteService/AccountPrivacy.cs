using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService
{
    public class AccountPrivacy
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("optOutOfPublicLeaderboards")]
        public bool OptOutOfPublicLeaderboards { get; set; }

        public AccountPrivacy()
        {
        }
        
        public AccountPrivacy(string accountId, bool optOutOfPublicLeaderboards)
        {
            AccountId = accountId;
            OptOutOfPublicLeaderboards = optOutOfPublicLeaderboards;
        }
    }
}