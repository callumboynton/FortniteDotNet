using System;
using System.Net;
using System.Web;
using System.Linq;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Attributes;
using System.Collections.Generic;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Services
{
    public class AccountService
    {
        /// <summary>
        /// Generates an OAuth session based on the provided parameters.
        /// </summary>
        /// <param name="grantType">The desired grant type for the session.</param>
        /// <param name="authClient">The desired auth client for the session.</param>
        /// <param name="fields">The fields for the provided grant type.</param>
        /// <param name="includePerms">Whether or not to include permissions.</param>
        /// <returns>A generated <see cref="OAuthSession"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided grant type requires fields and there weren't any provided.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided grant type is missing the appropriate fields.</exception>
        public async Task<OAuthSession> GenerateOAuthSession(GrantType grantType, AuthClient authClient, Dictionary<string, string> fields = null, bool includePerms = false)
        {
            // Sets up the encoded form data for when we make the request.
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add("grant_type", grantType.GetAttribute<ValueAttribute>().Value);
            query.Add("includePerms", includePerms.ToString().ToLower());
            
            // The grant type client_credentials doesn't actually require any form data, so if the provided grant type isn't client_credentials...
            if (grantType != GrantType.ClientCredentials)
            {
                // If fields doesn't have a count more than 0, throw an exception.
                if (fields is not {Count: > 0})
                    throw new ArgumentNullException(nameof(fields), "The provided GrantType requires fields, but there weren't any fields provided!");

                // Get the required fields for the provided grant type.
                var requiredFields = grantType.GetAttribute<RequiredFieldsAttribute>().Fields;
                
                // If the provided fields dictionary doesn't contain all of the required fields, throw an exception.
                if (requiredFields.Any(x => !fields.ContainsKey(x)))
                    throw new ArgumentException($"The provided GrantType requires the following fields: [{string.Join(", ", requiredFields)}]");
                
                // Add the fields to the encoded form data.
                foreach (var field in fields)
                    query.Add(field.Key, field.Value);
            }
            
            // We're using a using statement so that the initialised client is disposed of when the code block is exited.
            using var client = new WebClient
            {
                Headers =
                {
                    // This is required, otherwise Epic Games' API will think we haven't provided any data.
                    [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded",
                    // Get the basic authorization token for the provided auth client ("clientId:secret" encoded in Base64)
                    [HttpRequestHeader.Authorization] = $"basic {_authClients[authClient]}"
                }
            };
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<OAuthSession>(Endpoints.Accounts.OAuth.Token, query.ToString()).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Kills the provided <see cref="OAuthSession"/>, making it invalid and unusable.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to kill.</param>
        internal static async Task KillOAuthSession(OAuthSession oAuthSession)
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
            
            // Use our request helper to make a DELETE request. We don't return anything here, because this request returns 204 No Content, so there's nothing to return.
            await client.DeleteDataAsync(Endpoints.Accounts.OAuth.KillSession(oAuthSession.AccessToken)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Generates an exchange code with the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to generate an exchange code with.</param>
        /// <returns>A generated <see cref="ExchangeCode"/> for the account.</returns>
        internal static async Task<ExchangeCode> GenerateExchangeCode(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<ExchangeCode>(Endpoints.Accounts.OAuth.Exchange).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the account info of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The <see cref="AccountInfo"/> of the account.</returns>
        internal static async Task<AccountInfo> GetInformation(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<AccountInfo>(Endpoints.Accounts.Account(oAuthSession.AccountId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets a list of account information of the provided account IDs.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="ids">The account IDs to get the account information of.</param>
        /// <returns>A list of <see cref="AccountInfo"/> bound to the provided account IDs.</returns>
        public async Task<List<AccountInfo>> GetInformation(OAuthSession oAuthSession, params string[] ids)
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
            return await client.GetDataAsync<List<AccountInfo>>(Endpoints.Accounts.Account(ids)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the metadata of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>An <see cref="object"/> of the queried metadata bound to the account.</returns>
        internal static async Task<object> GetMetadata(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<object>(Endpoints.Accounts.Metadata(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of device auths of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of <see cref="DeviceAuth"/> that belongs to the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<DeviceAuth>> GetDeviceAuths(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<DeviceAuth>>(Endpoints.Accounts.DeviceAuths(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a specific device auth for an account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="deviceId">The device ID of the desired device auth.</param>
        /// <returns>The <see cref="DeviceAuth"/> bound to the provided device ID.</returns>
        internal static async Task<DeviceAuth> GetDeviceAuth(OAuthSession oAuthSession, string deviceId)
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
            return await client.GetDataAsync<DeviceAuth>(Endpoints.Accounts.DeviceAuth(oAuthSession.AccountId, deviceId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a <see cref="DeviceAuth"/> for an account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A generated <see cref="DeviceAuth"/> for the account.</returns>
        internal static async Task<DeviceAuth> CreateDeviceAuth(OAuthSession oAuthSession)
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
            
            // Use our request helper to make a POST request, and return the response data deserialized into the appropriate type.
            return await client.PostDataAsync<DeviceAuth>(Endpoints.Accounts.DeviceAuths(oAuthSession.AccountId), "").ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes the device auth bound to the provided device ID.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="deviceId">The device ID of the device auth to delete.</param>
        internal static async Task DeleteDeviceAuth(OAuthSession oAuthSession, string deviceId)
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
            
            // Use our request helper to make a DELETE request. We don't return anything here, because this request returns 204 No Content, so there's nothing to return.
            await client.DeleteDataAsync(Endpoints.Accounts.DeviceAuth(oAuthSession.AccountId, deviceId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all external auths for an account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of <see cref="ExternalAuth"/> of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<ExternalAuth>> GetExternalAuths(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<ExternalAuth>>(Endpoints.Accounts.ExternalAuths(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a specific external auth for an account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="type">The type of the desired external auth.</param>
        /// <returns>The <see cref="ExternalAuth"/> with the provided type of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<ExternalAuth> GetExternalAuth(OAuthSession oAuthSession, string type)
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
            return await client.GetDataAsync<ExternalAuth>(Endpoints.Accounts.ExternalAuth(oAuthSession.AccountId, type)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes the external auth bound to the provided type.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="type">The type of the external auth to delete.</param>
        internal static async Task DeleteExternalAuth(OAuthSession oAuthSession, string type)
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
            
            // Use our request helper to make a DELETE request. We don't return anything here, because this request returns 204 No Content, so there's nothing to return.
            await client.DeleteDataAsync(Endpoints.Accounts.ExternalAuth(oAuthSession.AccountId, type)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the account information of the account bound to the provided display name.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="displayName">The display name to query the account information of.</param>
        /// <returns>The <see cref="AccountInfo"/> of the account bound to the provided display name.</returns>
        public async Task<AccountInfo> GetInformationByDisplayName(OAuthSession oAuthSession, string displayName)
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
            return await client.GetDataAsync<AccountInfo>(Endpoints.Accounts.DisplayNameLookup(displayName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the account information of the account bound to the provided email.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="email">The email to query the account information of.</param>
        /// <returns>The <see cref="AccountInfo"/> of the account bound to the provided email.</returns>
        public async Task<AccountInfo> GetInformationByEmail(OAuthSession oAuthSession, string email)
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
            return await client.GetDataAsync<AccountInfo>(Endpoints.Accounts.EmailLookup(email)).ConfigureAwait(false);
        }

        /// <summary>
        /// A dictionary of auth clients and their basic authorization token ("clientId:secret" encoded in Base64).
        /// </summary>
        internal static readonly Dictionary<AuthClient, string> _authClients = new()
        {
            { AuthClient.PC, "ZWM2ODRiOGM2ODdmNDc5ZmFkZWEzY2IyYWQ4M2Y1YzY6ZTFmMzFjMjExZjI4NDEzMTg2MjYyZDM3YTEzZmM4NGQ=" },
            { AuthClient.iOS, "MzQ0NmNkNzI2OTRjNGE0NDg1ZDgxYjc3YWRiYjIxNDE6OTIwOWQ0YTVlMjVhNDU3ZmI5YjA3NDg5ZDMxM2I0MWE=" },
            { AuthClient.Android, "M2Y2OWU1NmM3NjQ5NDkyYzhjYzI5ZjFhZjA4YThhMTI6YjUxZWU5Y2IxMjIzNGY1MGE2OWVmYTY3ZWY1MzgxMmU=" },
            { AuthClient.Switch, "NTIyOWRjZDNhYzM4NDUyMDhiNDk2NjQ5MDkyZjI1MWI6ZTNiZDJkM2UtYmY4Yy00ODU3LTllN2QtZjNkOTQ3ZDIyMGM3" },
            { AuthClient.Launcher, "MzRhMDJjZjhmNDQxNGUyOWIxNTkyMTg3NmRhMzZmOWE6ZGFhZmJjY2M3Mzc3NDUwMzlkZmZlNTNkOTRmYzc2Y2Y=" }
        };
    }
}