using Newtonsoft.Json;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;

namespace FortniteDotNet.Models.Party
{
    public class PartyCreationInfo
    {
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("join_info")]
        public PartyJoinInfo JoinInfo { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        /// <summary>
        /// This constructor sets the property to the default values provided by Fortnite in order to create a party.
        /// </summary>
        /// <param name="xmppClient">The active XMPP Client so we can access its properties, like its Jabber ID.</param>
        public PartyCreationInfo(XMPPClient xmppClient)
        {
            Config = new()
            {
                {"join_confirmation", false},
                {"joinability", "OPEN"},
                {"max_size", 16} 
            };
            JoinInfo = new PartyJoinInfo(xmppClient, false);
            Meta = new()
            {
                {"urn:epic:cfg:party-type-id_s", "default"},
                {"urn:epic:cfg:build-id_s", "1:3:"},
                {"urn:epic:cfg:join-request-action_s", "Manual"},
                {"urn:epic:cfg:presence-perm_s", "Noone"},
                {"urn:epic:cfg:invite-perm_s", "Noone"},
                {"urn:epic:cfg:chat-enabled_b", "true"},
                {"urn:epic:cfg:accepting-members_b", "false"},
                {"urn:epic:cfg:not-accepting-members-reason_i", "0"}
            };
        }
    }
}