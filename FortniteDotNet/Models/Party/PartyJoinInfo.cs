using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;

namespace FortniteDotNet.Models.Party
{
    public class PartyJoinInfo
    {
        [JsonProperty("connection")]
        public PartyMemberConnection Connection { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);

        /// <summary>
        /// The default party join information.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to use for the join info.</param>
        /// <param name="isJoin">Is the join info for a joining party or a creating party?</param>
        public PartyJoinInfo(XMPPClient xmppClient, bool isJoin = true)
        {
            Connection = new PartyMemberConnection
            {
                Id = $"{xmppClient.Jid}/{xmppClient.Resource}",
                Meta = new()
                {
                    {"urn:epic:conn:platform_s", "WIN"},
                    {"urn:epic:conn:type_s", "game"}
                }
            };

            Meta = new()
            {
                {"urn:epic:member:dn_s", xmppClient.AuthSession.DisplayName}
            };

            if (!isJoin) 
                return;
            
            Connection.YieldLeadership = false;
            Meta.Add("urn:epic:member:joinrequestusers_j", new PartyJoinRequest(xmppClient).ToString());
        }
    }
}