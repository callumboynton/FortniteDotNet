using System;
using System.Net;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Attributes;
using System.Collections.Generic;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.Fortnite;
using FortniteDotNet.Payloads.Fortnite;
using FortniteDotNet.Models.Fortnite.Calendar;
using FortniteDotNet.Models.Fortnite.Storefront;

namespace FortniteDotNet.Services
{
    public class FortniteService
    {
        /// <summary>
        /// Executes the QueryProfile command which retrieves the profile data of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <returns>The <see cref="McpResponse"/> of the executed command.</returns>
        internal static async Task<McpResponse> QueryProfile(OAuthSession oAuthSession, Profile profile, int revision = -1)
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
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.QueryProfile(oAuthSession.AccountId, profileId, revision), "{}").ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the ClientQuestLogin command which retrieves the quest data of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <returns>The <see cref="McpResponse"/> of the executed command.</returns>
        internal static async Task<McpResponse> ClientQuestLogin(OAuthSession oAuthSession, Profile profile, int revision = -1)
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
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.ClientQuestLogin(oAuthSession.AccountId, profileId, revision), "{}").ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the MarkItemSeen command which marks an item within the profile as seen for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="profile">The profile to retrieve the profile data of.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <param name="payload">The payload for the request.</param>
        /// <returns>The <see cref="McpResponse"/> of the executed command.</returns>
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

        /// <summary>
        /// Executes the GiftCatalogEntry command which gifts an item to another user from the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <param name="payload">The payload for the request.</param>
        /// <returns>The <see cref="McpResponse"/> of the executed command.</returns>
        [Obsolete("Using this profile command directly may result in gifts being revoked, so proceed with caution!")]
        internal static async Task<McpResponse> GiftCatalogEntry(OAuthSession oAuthSession, GiftCatalogEntry payload, int revision = -1)
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<McpResponse>(Endpoints.Fortnite.Mcp.GiftCatalogEntry(oAuthSession.AccountId, revision),
                JsonConvert.SerializeObject(payload)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the privacy configuration of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The <see cref="AccountPrivacy"/> of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<AccountPrivacy> GetAccountPrivacy(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<AccountPrivacy>(Endpoints.Fortnite.AccountPrivacy(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the privacy configuration of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="optOutOfPublicLeaderboards">Should the account be shown on public leaderboards?</param>
        /// <returns>The <see cref="AccountPrivacy"/> of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<AccountPrivacy> SetAccountPrivacy(OAuthSession oAuthSession, bool optOutOfPublicLeaderboards)
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
            return await client.PostDataAsync<AccountPrivacy>(Endpoints.Fortnite.AccountPrivacy(oAuthSession.AccountId), 
                JsonConvert.SerializeObject(new AccountPrivacy
                {
                    AccountId = oAuthSession.AccountId,
                    OptOutOfPublicLeaderboards = optOutOfPublicLeaderboards
                })).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the current catalog.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The current <see cref="Catalog"/>.</returns>
        public async Task<Catalog> GetCatalog(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<Catalog>(Endpoints.Fortnite.Storefront.Catalog).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the current keychain.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The current keychain as a list of strings.</returns>
        public async Task<List<string>> GetKeychain(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<string>>(Endpoints.Fortnite.Storefront.Keychain).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the current timeline.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The current <see cref="Timeline"/>.</returns>
        public async Task<Timeline> GetTimeline(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<Timeline>(Endpoints.Fortnite.Timeline).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of system cloudstorage files.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of system <see cref="CloudstorageFile"/>s.</returns>
        public async Task<List<CloudstorageFile>> GetSystemCloudstorageFiles(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<CloudstorageFile>>(Endpoints.Fortnite.Cloudstorage.System).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the system cloudstorage file bound to the provided unique filename.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="uniqueFilename">The unique filename of the desired system cloudstorage file.</param>
        /// <returns>The system <see cref="CloudstorageFile"/> bound to the provided unique filename.</returns>
        public async Task<CloudstorageFile> GetSystemCloudstorageFile(OAuthSession oAuthSession, string uniqueFilename)
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
            return await client.GetDataAsync<CloudstorageFile>(Endpoints.Fortnite.Cloudstorage.SystemFile(uniqueFilename)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of user cloudstorage files of the account bound to the provided <see cref="OAuthSession"/>. 
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of user <see cref="CloudstorageFile"/>s of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<CloudstorageFile>> GetUserCloudstorageFiles(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<CloudstorageFile>>(Endpoints.Fortnite.Cloudstorage.User(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the user cloudstorage file bound to the provided unique filename of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="uniqueFilename">The unique filename of the desired user cloudstorage file.</param>
        /// <returns>The user <see cref="CloudstorageFile"/> bound to the provided unique filename.</returns>
        internal static async Task<List<CloudstorageFile>> GetUserCloudstorageFile(OAuthSession oAuthSession, string uniqueFilename)
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
            return await client.GetDataAsync<List<CloudstorageFile>>(Endpoints.Fortnite.Cloudstorage.UserFile(oAuthSession.AccountId, uniqueFilename)).ConfigureAwait(false);
        }
    }
}