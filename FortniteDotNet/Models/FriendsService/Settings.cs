using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService
{
    public class Settings
    {
        [JsonProperty("acceptInvites")]
        public string AcceptInvites { get; set; }

        [JsonProperty("mutualPrivacy")]
        public string MutualPrivacy { get; set; }
        
        internal Settings()
        {
        }

        public Settings(bool acceptInvites)
        {
            AcceptInvites = acceptInvites ? "public" : "private";
        }
    }
}