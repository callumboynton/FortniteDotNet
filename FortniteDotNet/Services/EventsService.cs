using System.Net;
using System.Web;
using FortniteDotNet.Util;
using System.Threading.Tasks;
using FortniteDotNet.Enums.Events;
using FortniteDotNet.Models.Events;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Services
{
    public class EventsService
    {
        /// <summary>
        /// Gets the event data for a specified player bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="region">The desired region to get the event data of.</param>
        /// <param name="platform">The desired platform to get the event data of.</param>
        /// <returns>The <see cref="EventData"/> for the provided region and platform.</returns>
        internal static async Task<EventData> GetEventData(OAuthSession oAuthSession, Region region, Platform platform)
        {
            // Sets up the encoded form data for when we make the request.
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add("region", region.ToString());
            query.Add("platform", platform.ToString());
            query.Add("teamAccountIds", oAuthSession.AccountId);
            
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
            return await client.GetDataAsync<EventData>(Endpoints.Events.EventData(oAuthSession.AccountId, query.ToString())).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the leaderboard data for a specified player bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="eventId">The desired event ID to retrieve the data from.</param>
        /// <param name="eventWindowId">The desired event window ID to retrieve the data from.</param>
        /// <returns>the <see cref="Leaderboard"/> of the provided event window ID for the player.</returns>
        internal static async Task<Leaderboard> GetLeaderboardData(OAuthSession oAuthSession, string eventId, string eventWindowId)
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
            return await client.GetDataAsync<Leaderboard>(Endpoints.Events.LeaderboardData(oAuthSession.AccountId, eventId, eventWindowId)).ConfigureAwait(false);
        }
    }
}