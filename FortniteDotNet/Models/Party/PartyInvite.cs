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

        /// <summary>
        /// Accepts a party invite.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to accept the invite for.</param>
        /// <exception cref="InvalidOperationException">Thrown if the invite is expired.</exception>
        public async Task Accept(XMPPClient xmppClient)
        {
            // If the expiry time has passed, this means the invite has expired.
            if (DateTime.Compare(DateTime.UtcNow, ExpiresAt) > 0)
                IsExpired = true;
            
            // If the invite has expired, throw an exception. 
            if (IsExpired)
                throw new InvalidOperationException($"Party invite to {PartyId} expired or was already accepted/declined!");

            // If our XMPP client's party is not null, leave it.
            if (xmppClient.CurrentParty != null)
                await PartyService.LeaveParty(xmppClient);
            
            // Join the party of the invite, and delete the ping.
            await PartyService.JoinParty(xmppClient, this);
            await PartyService.DeletePingById(xmppClient.AuthSession, SentBy);

            // The invite was accepted, so it has now expired.
            IsExpired = true;
        }

        /// <summary>
        /// Declines a party invite.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to decline the invite for.</param>
        /// <exception cref="InvalidOperationException">Thrown if the invite is expired.</exception>
        public async Task Decline(XMPPClient xmppClient)
        {
            // If the expiry time has passed, this means the invite has expired.
            if (DateTime.Compare(DateTime.UtcNow, ExpiresAt) > 0)
                IsExpired = true;
            
            // If the invite has expired, throw an exception. 
            if (IsExpired)
                throw new InvalidOperationException($"Party invite to {PartyId} expired or was already accepted/declined!");
            
            // Delete the ping.
            await PartyService.DeletePingById(xmppClient.AuthSession, SentBy);
            
            // The invite was declined, so it has now expired.
            IsExpired = true;
        }

        /// <summary>
        /// Sends an invite to the account bound to the provided account ID.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to send the invite from.</param>
        /// <param name="accountId">The account ID of the desired account to send the invite to.</param>
        public static async Task Send(XMPPClient xmppClient, string accountId)
            => await PartyService.SendInvite(xmppClient, accountId);
    }
}