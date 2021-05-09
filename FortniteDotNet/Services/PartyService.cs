using System;
using System.Net;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.XMPP.Payloads;

namespace FortniteDotNet.Services
{
    public class PartyService
    {
        internal static async Task<PartyInfo> CreateParty(OAuthSession oAuthSession, XMPPClient xmppClient)
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

            // Use our request helper to make a POST request.
            var response = xmppClient.CurrentParty = await client.PostDataAsync<PartyInfo>(Endpoints.Party.Parties,
                JsonConvert.SerializeObject(new PartyCreationInfo(xmppClient))).ConfigureAwait(false);

            await xmppClient.SendPresence(new Presence
            {
                Status = "Playing Fortnite",
                IsPlaying = true,
                IsJoinable = false,
                HasVoiceSupport = false,
                SessionId = "",
                Properties = new()
                {
                    {
                        "party.joininfodata.286331153_j", new Dictionary<string, object>
                        {
                            {"sourceId", oAuthSession.AccountId},
                            {"sourceDisplayName", oAuthSession.DisplayName},
                            {"sourcePlatform", "WIN"},
                            {"partyId", xmppClient.CurrentParty.Id},
                            {"partyTypeId", 286331153},
                            {"key", "k"},
                            {"appId", "Fortnite"},
                            {"buildId", "1:3:"},
                            {"partyFlags", -2024557306},
                            {"notAcceptingReason", 0},
                            {"pc", xmppClient.CurrentParty.Members.Count}
                        }
                    }
                }
            });

            return response;
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

            // Use our request helper to make a POST request.
            await client.PatchDataAsync(Endpoints.Party.QueryParty(partyInfo.Id),
                JsonConvert.SerializeObject(new PartyUpdate
                {
                    Config = new()
                    {
                        {"join_confirmation", partyInfo.Config["join_confirmation"]},
                        {"joinability", partyInfo.Config["joinability"]},
                        {"max_size", partyInfo.Config["max_size"]},
                        {"discoverability", partyInfo.Config["discoverability"]}
                    },
                    Meta = new PartyUpdateMeta
                    {
                        Delete = deleted ?? new(),
                        Update = updated
                    },
                    Revision = partyInfo.Revision
                })).ConfigureAwait(false);

            partyInfo.Revision++;
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

        internal static async Task JoinParty(OAuthSession oAuthSession, XMPPClient xmppClient, PartyInvite invite)
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

            // Use our request helper to make a POST request.
            var response = await client.PostDataAsync<PartyJoined>(Endpoints.Party.Join(invite.PartyId, oAuthSession.AccountId),
                JsonConvert.SerializeObject(new PartyJoinInfo
                {
                    Connection = new PartyMemberConnection
                    {
                        Id = xmppClient.Jid,
                        Meta = new()
                        {
                            {"urn:epic:conn:platform_s", "WIN"},
                            {"urn:epic:conn:type_s", "game"}
                        },
                        YieldLeadership = false
                    },
                    Meta = new()
                    {
                        {"urn:epic:member:dn_s", oAuthSession.DisplayName},
                        {
                            "urn:epic:member:joinrequestusers_j", JsonConvert.SerializeObject(new PartyJoinRequest
                            {
                                Users = new()
                                {
                                    new JoinRequestUser
                                    {
                                        Id = oAuthSession.AccountId,
                                        DisplayName = oAuthSession.DisplayName,
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

            xmppClient.CurrentParty = await GetParty(oAuthSession, response.PartyId);
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
    }
}