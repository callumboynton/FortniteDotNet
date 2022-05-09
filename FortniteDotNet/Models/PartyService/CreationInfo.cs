using System;
using System.Collections.Generic;
using FortniteDotNet.Xmpp;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.PartyService
{
    public class CreationInfo
    {
        [JsonProperty("config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("join_info")]
        public JoinInfo JoinInfo { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        public CreationInfo(XmppClient xmppClient)
        {
            if (xmppClient == null)
                throw new ArgumentNullException(nameof(xmppClient));
            
            Config = new Dictionary<string, object>
            {
                {"join_confirmation", false},
                {"joinability", "OPEN"},
                {"max_size", 16} 
            };
            JoinInfo = new JoinInfo(xmppClient);
            Meta = new Dictionary<string, string>
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

    public class JoinInfo
    {
        [JsonProperty("connection")]
        public Connection Connection { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        public JoinInfo(XmppClient xmppClient)
        {
            if (xmppClient == null)
                throw new ArgumentNullException(nameof(xmppClient));
            
            Connection = new Connection
            {
                Id = $"{xmppClient.JabberId}/{xmppClient.Resource}",
                Meta = new Dictionary<string, string>
                {
                    { "urn:epic:conn:platform_s", "WIN" },
                    { "urn:epic:conn:type_s", "game" }
                }
            };

            Meta = new Dictionary<string, string>
            {
                { "urn:epic:member:dn_s", xmppClient.AuthSession.DisplayName }
            };

        }
    }
}