using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;

namespace FortniteDotNet.Models.Party
{
    public class PartyInvite
    {
        [JsonProperty("party_id")]
        public string PartyId { get; set; }

        [JsonProperty("sent_by")]
        public string SentBy { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("sent_to")]
        public string SentTo { get; set; }

        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonIgnore]
        public bool IsExpired { get; set; }

        public async Task Accept(XMPPClient xmppClient)
        {
            if (DateTime.Compare(DateTime.UtcNow, ExpiresAt) > 0)
                IsExpired = true;
            
            if (IsExpired)
                throw new InvalidOperationException($"Party invite to {PartyId} expired or was already accepted/declined!");

            if (xmppClient.CurrentParty != null)
                await PartyService.LeaveParty(xmppClient);
            
            await PartyService.JoinParty(xmppClient, this);
            await PartyService.DeletePingById(xmppClient.AuthSession, SentBy);

            IsExpired = true;
        }

        public async Task Decline(XMPPClient xmppClient)
        {
            if (DateTime.Compare(DateTime.UtcNow, ExpiresAt) > 0)
                IsExpired = true;
            
            if (IsExpired)
                throw new InvalidOperationException($"Party invite to {PartyId} expired or was already accepted/declined!");
            
            await PartyService.DeletePingById(xmppClient.AuthSession, SentBy);

            IsExpired = true;
        }

        public static async Task Send(XMPPClient xmppClient, string accountId)
            => await PartyService.SendInvite(xmppClient, accountId);
    }
}