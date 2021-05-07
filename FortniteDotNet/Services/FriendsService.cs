using System.Net;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using FortniteDotNet.Models.Friends;
using FortniteDotNet.Models.Accounts;
using Newtonsoft.Json;

namespace FortniteDotNet.Services
{
    public class FriendsService
    {
        /// <summary>
        /// Gets the friends summary of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The <see cref="Summary"/> of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<Summary> GetSummary(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<Summary>(Endpoints.Friends.Summary(oAuthSession.AccountId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets a list of accepted friends for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of accepted <see cref="Friend"/>s for the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<Friend>> GetAcceptedFriends(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<Friend>>(Endpoints.Friends.AllFriends(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a friend bound to the provided account ID from the friends list of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the desired friend.</param>
        /// <returns>The <see cref="Friend"/> bound to the provided account ID from the friends list of the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<Friend> GetFriend(OAuthSession oAuthSession, string friendId)
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
            return await client.GetDataAsync<Friend>(Endpoints.Friends.Friend(oAuthSession.AccountId, friendId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Sends or accepts a friend request with the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to send or accept the request to/from.</param>
        internal static async Task SendOrAcceptFriendRequest(OAuthSession oAuthSession, string friendId)
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
            await client.PostDataAsync(Endpoints.Friends.Friend(oAuthSession.AccountId, friendId), "").ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes a friend or a friend request with the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to remove.</param>
        internal static async Task DeleteFriendOrRequest(OAuthSession oAuthSession, string friendId)
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
            
            // Use our request helper to make a DELETE request.
            await client.DeleteDataAsync(Endpoints.Friends.Friend(oAuthSession.AccountId, friendId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the alias for the friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to update the alias for.</param>
        /// <param name="newAlias">The desired alias.</param>
        internal static async Task UpdateFriendAlias(OAuthSession oAuthSession, string friendId, string newAlias)
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
            
            // Use our request helper to make a PUT request.
            await client.PutDataAsync(Endpoints.Friends.Alias(oAuthSession.AccountId, friendId), newAlias).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Removes the alias for the friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to remove the alias for.</param>
        internal static async Task RemoveFriendAlias(OAuthSession oAuthSession, string friendId)
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
            
            // Use our request helper to make a DELETE request.
            await client.DeleteDataAsync(Endpoints.Friends.Alias(oAuthSession.AccountId, friendId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the note for the friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to update the note for.</param>
        /// <param name="newNote">The desired note.</param>
        internal static async Task UpdateFriendNote(OAuthSession oAuthSession, string friendId, string newNote)
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
            
            // Use our request helper to make a PUT request.
            await client.PutDataAsync(Endpoints.Friends.Note(oAuthSession.AccountId, friendId), newNote).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Removes the note for the friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the account to remove the note for.</param>
        internal static async Task RemoveFriendNote(OAuthSession oAuthSession, string friendId)
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
            
            // Use our request helper to make a DELETE request.
            await client.DeleteDataAsync(Endpoints.Friends.Note(oAuthSession.AccountId, friendId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets a list of incoming friend requests for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of incoming <see cref="Friend"/>s for the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<Friend>> GetIncomingFriendRequests(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<Friend>>(Endpoints.Friends.Incoming(oAuthSession.AccountId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets a list of outgoing friend requests for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of incoming <see cref="Friend"/>s for the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<Friend>> GetOutgoingFriendRequests(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<Friend>>(Endpoints.Friends.Outgoing(oAuthSession.AccountId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the blocklist of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>A list of <see cref="BlockedFriend"/>s for the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<List<BlockedFriend>> GetBlocklist(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<List<BlockedFriend>>(Endpoints.Friends.Blocklist(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Blocks a friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the friend to block.</param>
        internal static async Task BlockFriend(OAuthSession oAuthSession, string friendId)
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
            await client.PostDataAsync(Endpoints.Friends.Block(oAuthSession.AccountId, friendId), "").ConfigureAwait(false);
        }
        
        /// <summary>
        /// Unblocks a friend bound to the provided account ID for the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="friendId">The account ID of the friend to unblock.</param>
        internal static async Task UnblockFriend(OAuthSession oAuthSession, string friendId)
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
            
            // Use our request helper to make a DELETE request.
            await client.DeleteDataAsync(Endpoints.Friends.Block(oAuthSession.AccountId, friendId)).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the friend settings of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <returns>The <see cref="Settings"/> for the account bound to the provided <see cref="OAuthSession"/>.</returns>
        internal static async Task<Settings> GetFriendSettings(OAuthSession oAuthSession)
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
            return await client.GetDataAsync<Settings>(Endpoints.Friends.Settings(oAuthSession.AccountId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the friend settings of the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="acceptInvites">Should the account auto-decline incoming friend requests?</param>
        internal static async Task UpdateFriendSettings(OAuthSession oAuthSession, bool acceptInvites)
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
            
            // Use our request helper to make a PUT request.
            await client.PutDataAsync(Endpoints.Friends.Settings(oAuthSession.AccountId), 
                JsonConvert.SerializeObject(new Settings
                {
                    AcceptInvites = acceptInvites ? "public" : "private"
                })).ConfigureAwait(false);
        }
    }
}