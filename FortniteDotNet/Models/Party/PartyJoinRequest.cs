using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;

namespace FortniteDotNet.Models.Party
{
    public class PartyJoinRequest
    {
        [JsonProperty("users")]
        public List<JoinRequestUser> Users { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);

        /// <summary>
        /// The default party join request.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to use for the join request.</param>
        public PartyJoinRequest(XMPPClient xmppClient)
        {
            Users = new()
            {
                new JoinRequestUser
                {
                    Id = xmppClient.AuthSession.AccountId,
                    DisplayName = xmppClient.AuthSession.DisplayName,
                    Platform = "WIN",
                    Data = new()
                    {
                        {"CrossplayReference", "1"},
                        {"SubGame_u", "1"}
                    }
                }
            };
        }
    }

    public class JoinRequestUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("dn")]
        public string DisplayName { get; set; }

        [JsonProperty("plat")]
        public string Platform { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
}