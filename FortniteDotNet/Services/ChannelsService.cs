using System.Net;
using System.Web;
using Newtonsoft.Json;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Attributes;
using System.Collections.Generic;
using FortniteDotNet.Enums.Channels;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.Channels;
using FortniteDotNet.Payloads.Channels;

namespace FortniteDotNet.Services
{
    public class ChannelsService
    {
        // The base endpoint for Epic Games' production channels service.
        private const string BASE_URL = "https://channels-public-service-prod.ol.epicgames.com";

        /// <summary>
        /// Gets a user setting with the provided setting key of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="settingKey">The setting key of the desired user setting.</param>
        /// <returns>The <see cref="UserSetting"/> bound to the provided setting key.</returns>
        internal static async Task<UserSetting> GetUserSetting(OAuthSession oAuthSession, SettingKey settingKey)
        {
            // Get the value of our enum. We're using enums for data validation purposes and to prevent erroneous responses.
            var setting = settingKey.GetAttribute<ValueAttribute>().Value;
            
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
            return await client.GetDataAsync<UserSetting>($"{BASE_URL}/api/v1/user/{oAuthSession.AccountId}/setting/{setting}").ConfigureAwait(false);
        }

        /// <summary>
        /// Updates a user setting with the provided setting key of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="settingKey">The setting key of the user setting to update.</param>
        /// <param name="payload">The payload for the request.</param>
        internal static async Task UpdateUserSetting(OAuthSession oAuthSession, SettingKey settingKey, UpdateUserSetting payload)
        {
            // Get the value of our enum. We're using enums for data validation purposes and to prevent erroneous responses.
            var setting = settingKey.GetAttribute<ValueAttribute>().Value;
            
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
            
            // Use our request helper to make a GET request, and return the response data deserialized into the appropriate type.
            await client.PutDataAsync<UserSetting>($"{BASE_URL}/api/v1/user/{oAuthSession.AccountId}/setting/{setting}",
                JsonConvert.SerializeObject(payload)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the available setting values for the provided setting key.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="settingKey">The setting key of the user setting to get the available values for.</param>
        /// <returns>A list of available setting values.</returns>
        internal static async Task<List<string>> GetAvailableSettingValues(OAuthSession oAuthSession, SettingKey settingKey)
        {
            // Get the value of our enum. We're using enums for data validation purposes and to prevent erroneous responses.
            var setting = settingKey.GetAttribute<ValueAttribute>().Value;
            
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
            return await client.GetDataAsync<List<string>>($"{BASE_URL}/api/v1/user/{oAuthSession.AccountId}/setting/{setting}/available").ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of user settings of the provided account IDs.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="settingKey">The setting key of the desired user settings.</param>
        /// <param name="ids">The account IDs to get the user settings of.</param>
        /// <returns>A list of <see cref="UserSetting"/> bound to the provided account IDs and the provided setting key.</returns>
        public async Task<List<UserSetting>> GetUserSettings(OAuthSession oAuthSession, SettingKey settingKey, params string[] ids)
        {
            // Get the value of our enum. We're using enums for data validation purposes and to prevent erroneous responses.
            var setting = settingKey.GetAttribute<ValueAttribute>().Value;
            
            // Sets up the encoded form data for when we make the request.
            var query = HttpUtility.ParseQueryString(string.Empty);
            
            // Add the provided account ID's to the query.
            foreach (var id in ids)
                query.Add("accountId", id);
            
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
            return await client.GetDataAsync<List<UserSetting>>($"{BASE_URL}/api/v1/user/setting/{setting}?{query}").ConfigureAwait(false);
        }
    }
}