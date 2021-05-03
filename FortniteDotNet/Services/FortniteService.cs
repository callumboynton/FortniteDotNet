﻿using System.Net;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Attributes;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Payloads.Fortnite;
using FortniteDotNet.Models.Fortnite.Mcp;

namespace FortniteDotNet.Services
{
    public class FortniteService
    {
        /// <summary>
        /// Executes the QueryProfile command which retrieves the profile data of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <returns>The <see cref="McpResponse"/> of the specified profile.</returns>
        public async Task<McpResponse> QueryProfile(OAuthSession oAuthSession, Profile profile)
        {
            var profileId = profile.GetAttribute<ValueAttribute>().Value;
            
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.QueryProfile(oAuthSession.AccountId, profileId), "{}").ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the ClientQuestLogin command which retrieves the quest data of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <returns></returns>
        internal static async Task<McpResponse> ClientQuestLogin(OAuthSession oAuthSession, Profile profile)
        {
            var profileId = profile.GetAttribute<ValueAttribute>().Value;
            
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.ClientQuestLogin(oAuthSession.AccountId, profileId), "{}").ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the MarkItemSeen command which marks an item within the profile as seen for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <param name="payload">The payload for the request.</param>
        /// <returns></returns>
        internal static async Task<McpResponse> MarkItemSeen(OAuthSession oAuthSession, Profile profile, MarkItemSeen payload, int revision = -1)
        {
            var profileId = profile.GetAttribute<ValueAttribute>().Value;
            
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.MarkItemSeen(oAuthSession.AccountId, profileId, revision),
                JsonConvert.SerializeObject(payload)).ConfigureAwait(false);
        }

        internal static async Task<McpResponse> GiftCatalogEntry(OAuthSession oAuthSession, Profile profile, GiftCatalogEntry payload, int revision = -1)
        {
            var profileId = profile.GetAttribute<ValueAttribute>().Value;
            
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.GiftCatalogEntry(oAuthSession.AccountId, profileId, revision),
                JsonConvert.SerializeObject(payload)).ConfigureAwait(false);
        }
    }
}