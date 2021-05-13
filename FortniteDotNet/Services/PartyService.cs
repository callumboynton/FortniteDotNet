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

        internal static async Task InitParty(XMPPClient xmppClient, bool create = true)
        {
            var summary = await GetSummary(xmppClient.AuthSession);
            xmppClient.CurrentParty = summary.Current.FirstOrDefault();

            if (create && xmppClient.CurrentParty != null)
                await LeaveParty(xmppClient);

            if (xmppClient.CurrentParty == null)
                await CreateParty(xmppClient);
        }
        
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
            
            await xmppClient.JoinPartyChat();
            
            return xmppClient.CurrentParty;
        }
        
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
                    JsonConvert.SerializeObject(new PartyJoinInfo
                    {
                        Connection = new PartyMemberConnection
                        {
                            Id = $"{xmppClient.Jid}/{xmppClient.Resource}",
                            Meta = new()
                            {
                                {"urn:epic:conn:platform_s", "WIN"},
                                {"urn:epic:conn:type_s", "game"}
                            },
                            YieldLeadership = false
                        },
                        Meta = new()
                        {
                            {"urn:epic:member:dn_s", xmppClient.AuthSession.DisplayName},
                            {
                                "urn:epic:member:joinrequestusers_j", JsonConvert.SerializeObject(new PartyJoinRequest
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
                                    }
                                })
                            }
                        }
                    })).ConfigureAwait(false);

                xmppClient.CurrentParty = await GetParty(xmppClient.AuthSession, response.PartyId);
                await xmppClient.JoinPartyChat();
            }
            catch (EpicException ex)
            {
                if (ex.ErrorCode != "errors.com.epicgames.social.party.user_has_party")
                {
                    await InitParty(xmppClient);
                    throw;
                }
                
                await InitParty(xmppClient);
                goto start;
            }
        }
        
        internal static async Task LeaveParty(XMPPClient xmppClient, bool createNew = false)
        {
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
            }
            catch (EpicException ex)
            {
                if (ex.ErrorCode != "errors.com.epicgames.social.party.party_not_found")
                    throw;

                await InitParty(xmppClient);
            }
            
            xmppClient.CurrentParty = null;

            if (createNew)
                await CreateParty(xmppClient);
        }

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
                    JsonConvert.SerializeObject(new PartyUpdate
                    {
                        Config = new()
                        {
                            {"join_confirmation", partyInfo.Config["join_confirmation"]},
                            {"joinability", partyInfo.Config["joinability"]},
                            {"max_size", partyInfo.Config["max_size"]}
                        },
                        Meta = new PartyUpdateMeta
                        {
                            Delete = deleted ?? new(),
                            Update = updated
                        },
                        Revision = partyInfo.Revision
                    })).ConfigureAwait(false);

                partyInfo.Revision++;
                partyInfo.UpdatedAt = DateTime.Now;
            }
            catch (EpicException ex)
            {
                if (ex.ErrorCode != "errors.com.epicgames.social.party.stale_revision")
                    throw;
                
                partyInfo.Revision = Convert.ToInt32(ex.MessageVars[1]);
                goto start;
            }
        }

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
                    JsonConvert.SerializeObject(new PartyMemberUpdate
                    {
                        Delete = deleted ?? new(),
                        Update = updated ?? PartyMember.SchemaMeta,
                        Revision = partyMember.Revision
                    })).ConfigureAwait(false);

                partyMember.Revision++;
                partyMember.UpdatedAt = DateTime.Now;
            }
            catch (EpicException ex)
            {
                if (ex.ErrorCode != "errors.com.epicgames.social.party.stale_revision")
                    throw;
                
                partyMember.Revision = Convert.ToInt32(ex.MessageVars[1]);
                goto start;
            }
        }
        
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

        public async Task KickMember(XMPPClient xmppClient, PartyMember partyMember)
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

        public async Task PromoteMember(XMPPClient xmppClient, PartyMember partyMember)
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