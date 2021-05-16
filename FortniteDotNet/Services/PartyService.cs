using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.Common;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Services
{
    public class PartyService
    {
        /// <summary>
        /// Gets the party information of the party bound to the provided party ID.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="partyId">The party ID of the desired party.</param>
        /// <returns>The party information of the party bound to the provided party ID.</returns>
        internal static async Task<PartyInfo> GetParty(OAuthSession oAuthSession, string partyId)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a GET request, and return the response data deserialized into the appropriate type.
            return await client.GetDataAsync<PartyInfo>(Endpoints.Party.QueryParty(partyId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the summary of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The <see cref="PartySummary"/> of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<PartySummary> GetSummary(OAuthSession oAuthSession)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a GET request, and return the response data deserialized into the appropriate type.
            return await client.GetDataAsync<PartySummary>(Endpoints.Party.Summary(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Initialises a new party for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to use.</param>
        internal static async Task InitParty(XMPPClient xmppClient)
        {
            // Get the party summary of the account bound to the provided XMPP client, and update its current party.
            var summary = await GetSummary(xmppClient.AuthSession);
            xmppClient.CurrentParty = summary.Current.FirstOrDefault();

            // If the current party is not null, leave the party.
            if (xmppClient.CurrentParty != null)
                await LeaveParty(xmppClient);

            // Create the party.
            if (xmppClient.CurrentParty == null)
                await CreateParty(xmppClient);
        }
        
        /// <summary>
        /// Creates a party for the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to create a party for.</param>
        /// <returns>The <see cref="PartyInfo"/> of the created party.</returns>
        internal static async Task<PartyInfo> CreateParty(XMPPClient xmppClient)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is so Epic Games' API knows what kind of data we're providing.
                    [HttpRequestHeader.ContentType] = "application/json",
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a POST request.
            xmppClient.CurrentParty = await client.PostDataAsync<PartyInfo>(Endpoints.Party.Parties,
                JsonConvert.SerializeObject(new PartyCreationInfo(xmppClient))).ConfigureAwait(false);
            
            // Join the party chat.
            await xmppClient.JoinPartyChat();
            
            // Return the current party.
            return xmppClient.CurrentParty;
        }
        
        /// <summary>
        /// Joins the party of the provided <see cref="PartyInvite"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to join the party from.</param>
        /// <param name="invite">The <see cref="PartyInvite"/> to accept.</param>
        internal static async Task JoinParty(XMPPClient xmppClient, PartyInvite invite)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is so Epic Games' API knows what kind of data we're providing.
                    [HttpRequestHeader.ContentType] = "application/json",
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            start:
            try
            {
                // Use our request helper to make a POST request.
                var response = await client.PostDataAsync<PartyJoined>(Endpoints.Party.Join(invite.PartyId, xmppClient.AuthSession.AccountId),
                    new PartyJoinInfo(xmppClient).ToString()).ConfigureAwait(false);

                // Set the XMPP clients current party to the party we just joined, then join the party chat.
                xmppClient.CurrentParty = await GetParty(xmppClient.AuthSession, response.PartyId);
                await xmppClient.JoinPartyChat();
            }
            catch (EpicException ex)
            {
                // If the error code DOESN'T indicate we're already in a party, throw the exception.
                if (ex.ErrorCode != "errors.com.epicgames.social.party.user_has_party")
                {
                    await InitParty(xmppClient);
                    throw;
                }
                
                // Else, restart the process.
                await InitParty(xmppClient);
                goto start;
            }
        }
        
        /// <summary>
        /// Leaves the current party of the provided <see cref="XMPPClient"/>.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to leave the party from.</param>
        internal static async Task LeaveParty(XMPPClient xmppClient)
        {
            // Leave the party chat.
            await xmppClient.LeavePartyChat();
            
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            try
            {
                // Use our request helper to make a DELETE request.
                await client.DeleteDataAsync(Endpoints.Party.Member(xmppClient.CurrentParty.Id, xmppClient.AuthSession.AccountId)).ConfigureAwait(false);
                xmppClient.CurrentParty = null;
            }
            catch (EpicException ex)
            {
                // If the error code DOESN'T indicate there was no party found, throw the exception.
                if (ex.ErrorCode != "errors.com.epicgames.social.party.party_not_found")
                    throw;
            }
            
            await InitParty(xmppClient);
        }

        /// <summary>
        /// Updates the party bound to the provided <see cref="PartyInfo"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="partyInfo">The <see cref="PartyInfo"/> of the party to update.</param>
        /// <param name="updated">The updated party meta.</param>
        /// <param name="deleted">The deleted party meta.</param>
        internal static async Task UpdateParty(OAuthSession oAuthSession, PartyInfo partyInfo, Dictionary<string, object> updated = null, List<string> deleted = null)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is so Epic Games' API knows what kind of data we're providing.
                    [HttpRequestHeader.ContentType] = "application/json",
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            start:
            try
            {
                // Use our request helper to make a POST request.
                await client.PatchDataAsync(Endpoints.Party.QueryParty(partyInfo.Id),
                    new PartyUpdate(partyInfo, updated, deleted).ToString()).ConfigureAwait(false);

                // Increment the party revision and last updated time.
                partyInfo.Revision++;
                partyInfo.UpdatedAt = DateTime.Now;
            }
            catch (EpicException ex)
            {
                // If the error code DOESN'T indicate the revision was stale, throw the exception.
                if (ex.ErrorCode != "errors.com.epicgames.social.party.stale_revision")
                    throw;
                
                // Update the revision to the one from the exception, then restart the process.
                partyInfo.Revision = Convert.ToInt32(ex.MessageVars[1]);
                goto start;
            }
        }

        /// <summary>
        /// Updates the party member bound to the provided <see cref="PartyMember"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="partyMember">The <see cref="PartyMember"/> to update.</param>
        /// <param name="partyId">The party ID of the party the member is in.</param>
        /// <param name="updated">The updated party meta.</param>
        /// <param name="deleted">The deleted party meta.</param>
        internal static async Task UpdateMember(OAuthSession oAuthSession, PartyMember partyMember, string partyId, Dictionary<string, object> updated = null, List<string> deleted = null)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is so Epic Games' API knows what kind of data we're providing.
                    [HttpRequestHeader.ContentType] = "application/json",
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };
            
            start:
            try
            {
                // Use our request helper to make a POST request.
                await client.PatchDataAsync(Endpoints.Party.MemberMeta(partyId, oAuthSession.AccountId),
                    new PartyMemberUpdate(partyMember, updated, deleted).ToString()).ConfigureAwait(false);

                // Increment the party revision and last updated time.
                partyMember.Revision++;
                partyMember.UpdatedAt = DateTime.Now;
            }
            catch (EpicException ex)
            {
                // If the error code DOESN'T indicate the revision was stale, throw the exception.
                if (ex.ErrorCode != "errors.com.epicgames.social.party.stale_revision")
                    throw;
                
                // Update the revision to the one from the exception, then restart the process.
                partyMember.Revision = Convert.ToInt32(ex.MessageVars[1]);
                goto start;
            }
        }
        
        /// <summary>
        /// Confirms a member for a party.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="partyId">The party ID of the party that the member is in.</param>
        /// <param name="memberId">The account ID of the member to confirm.</param>
        internal static async Task ConfirmMember(OAuthSession oAuthSession, string partyId, string memberId)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a POST request.
            await client.PostDataAsync(Endpoints.Party.ConfirmMember(partyId, memberId), "").ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets a list of <see cref="PartyInfo"/>s which indicates the client has pending invites.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="pingerId">The account ID of the pinger.</param>
        /// <returns></returns>
        internal static async Task<List<PartyInfo>> GetPartyPings(OAuthSession oAuthSession, string pingerId)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a GET request, and return the response data deserialized into the appropriate type.
            return await client.GetDataAsync<List<PartyInfo>>(Endpoints.Party.PartyPings(oAuthSession.AccountId, pingerId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes a ping bound to the provided sender ID.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="senderId">The account ID of the ping sender.</param>
        internal static async Task DeletePingById(OAuthSession oAuthSession, string senderId)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {oAuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a DELETE request, and return the response data deserialized into the appropriate type.
            await client.DeleteDataAsync(Endpoints.Party.UserPings(oAuthSession.AccountId, senderId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends an invite from the provided <see cref="XMPPClient"/> to the account bound to the provided account ID.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to send the invite from.</param>
        /// <param name="accountId">The account ID of the account to send the invite to.</param>
        internal static async Task SendInvite(XMPPClient xmppClient, string accountId)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is so Epic Games' API knows what kind of data we're providing.
                    [HttpRequestHeader.ContentType] = "application/json",
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a POST request.
            await client.PostDataAsync(Endpoints.Party.Invite(xmppClient.CurrentParty.Id, accountId), 
                JsonConvert.SerializeObject(xmppClient.CurrentParty.Config)).ConfigureAwait(false);
        }

        /// <summary>
        /// Kicks a member from a party.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to kick the member from.</param>
        /// <param name="partyMember">The <see cref="PartyMember"/> to kick.</param>
        internal static async Task KickMember(XMPPClient xmppClient, PartyMember partyMember)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a DELETE request.
            await client.DeleteDataAsync(Endpoints.Party.Member(xmppClient.CurrentParty.Id, partyMember.Id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Promotes a member in a party.
        /// </summary>
        /// <param name="xmppClient">The <see cref="XMPPClient"/> to promote the member from.</param>
        /// <param name="partyMember">The <see cref="PartyMember"/> to promote.</param>
        internal static async Task PromoteMember(XMPPClient xmppClient, PartyMember partyMember)
        {
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // Set the Authorization header to the access token from the provided OAuthSession.
                    [HttpRequestHeader.Authorization] = $"bearer {xmppClient.AuthSession.AccessToken}"
                }
            };

            // Use our request helper to make a DELETE request.
            await client.PostDataAsync(Endpoints.Party.Promote(xmppClient.CurrentParty.Id, partyMember.Id), "").ConfigureAwait(false);
        }
    }
}