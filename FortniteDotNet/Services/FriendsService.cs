using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Models.FriendsService;
using FortniteDotNet.Models.FriendsService.Legacy;

using Friend = FortniteDotNet.Models.FriendsService.Friend;
using LegacyFriend = FortniteDotNet.Models.FriendsService.Legacy.Friend;

namespace FortniteDotNet.Services
{
    public interface IFriendsService
    {
        #region Legacy Friends Implementation
        
        public Task<List<LegacyFriend>> GetLegacyFriends(AuthSession authSession, bool includePending = true);
        public Task<Blocklist> GetLegacyBlocklist(AuthSession authSession);
        
        public Task<LegacyFriend> GetLegacyFriend(AuthSession authSession, string friendId);
        public Task AddLegacyFriend(AuthSession authSession, string friendId);
        public Task RemoveLegacyFriend(AuthSession authSession, string friendId);
        public Task BlockLegacyFriend(AuthSession authSession, string friendId);
        public Task UnblockLegacyFriend(AuthSession authSession, string friendId);
        
        #endregion

        #region Current Friends Implementation
        
        public Task<Summary> GetSummary(AuthSession authSession);
        public Task<List<AcceptedFriend>> GetFriends(AuthSession authSession);
        public Task<List<Friend>> GetIncoming(AuthSession authSession);
        public Task<List<Friend>> GetOutgoing(AuthSession authSession);
        public Task<List<BaseFriend>> GetBlocklist(AuthSession authSession);
        public Task<List<SuggestedFriend>> GetSuggested(AuthSession authSession);
        public Task<List<RecentPlayer>> GetRecentPlayers(AuthSession authSession);
        
        public Task<Friend> GetFriend(AuthSession authSession, string friendId);
        public Task AddFriend(AuthSession authSession, string friendId);
        public Task RemoveFriend(AuthSession authSession, string friendId);
        public Task BlockFriend(AuthSession authSession, string friendId);
        public Task UnblockFriend(AuthSession authSession, string friendId);
        
        public Task SetFriendAlias(AuthSession authSession, string friendId, string alias);
        public Task RemoveFriendAlias(AuthSession authSession, string friendId);
        
        public Task SetFriendNote(AuthSession authSession, string friendId, string note);
        public Task RemoveFriendNote(AuthSession authSession, string friendId);

        public Task<Settings> GetSettings(AuthSession authSession);
        public Task<Settings> SetSettings(AuthSession authSession, Settings payload);
        
        #endregion
    }
    
    public class FriendsService : IFriendsService
    {
        private readonly Uri _baseUri = new Uri("https://friends-public-service-prod.ol.epicgames.com/friends/api/");
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<List<LegacyFriend>> GetLegacyFriends(AuthSession authSession, bool includePending)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/friends/{authSession.AccountId}?includePending={includePending}"));
            return await response.Handle<List<LegacyFriend>>();
        }

        public async Task<Blocklist> GetLegacyBlocklist(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/blocklist/{authSession.AccountId}"));
            return await response.Handle<Blocklist>();
        }

        public async Task<LegacyFriend> GetLegacyFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/friends/{authSession.AccountId}/{friendId}"));
            return await response.Handle<LegacyFriend>();
        }

        public async Task AddLegacyFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"public/friends/{authSession.AccountId}/{friendId}"), null);
            await response.Handle();
        }

        public async Task RemoveLegacyFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"public/friends/{authSession.AccountId}/{friendId}"));
            await response.Handle();
        }

        public async Task BlockLegacyFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"public/blocklist/{authSession.AccountId}/{friendId}"), null);
            await response.Handle();
        }

        public async Task UnblockLegacyFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"public/blocklist/{authSession.AccountId}/{friendId}"));
            await response.Handle();
        }

        public async Task<Summary> GetSummary(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/summary"));
            return await response.Handle<Summary>();
        }

        public async Task<List<AcceptedFriend>> GetFriends(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends"));
            return await response.Handle<List<AcceptedFriend>>();
        }

        public async Task<List<Friend>> GetIncoming(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/incoming"));
            return await response.Handle<List<Friend>>();
        }

        public async Task<List<Friend>> GetOutgoing(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/outgoing"));
            return await response.Handle<List<Friend>>();
        }

        public async Task<List<BaseFriend>> GetBlocklist(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/blocklist"));
            return await response.Handle<List<BaseFriend>>();
        }

        public async Task<List<SuggestedFriend>> GetSuggested(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/suggested"));
            return await response.Handle<List<SuggestedFriend>>();
        }

        public async Task<List<RecentPlayer>> GetRecentPlayers(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/recent/fortnite"));
            return await response.Handle<List<RecentPlayer>>();
        }

        public async Task<Friend> GetFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}"));
            return await response.Handle<Friend>();
        }

        public async Task AddFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}"), null);
            await response.Handle();
        }

        public async Task RemoveFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}"));
            await response.Handle();
        }

        public async Task BlockFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/blocklist/{friendId}"), null);
            await response.Handle();
        }

        public async Task UnblockFriend(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/blocklist/{friendId}"));
            await response.Handle();
        }

        public async Task SetFriendAlias(AuthSession authSession, string friendId, string alias)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            if (alias == null)
                throw new ArgumentNullException(nameof(alias));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("The parameter must have some value to it.", nameof(alias));
            if (alias.Length <= 0 || alias.Length >= 16)
                throw new ArgumentException("The parameter length must be more than 0 and less than or equal to 16.", nameof(alias));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}/alias"), new StringContent(alias));
            await response.Handle();
        }

        public async Task RemoveFriendAlias(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}/alias"));
            await response.Handle();
        }

        public async Task SetFriendNote(AuthSession authSession, string friendId, string note)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            if (note == null)
                throw new ArgumentNullException(nameof(note));
            if (string.IsNullOrWhiteSpace(note))
                throw new ArgumentException("The parameter must have some value to it.", nameof(note));
            if (note.Length <= 0 || note.Length >= 255)
                throw new ArgumentException("The parameter length must be more than 0 and less than or equal to 16.", nameof(note));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}/note"), new StringContent(note));
            await response.Handle();
        }

        public async Task RemoveFriendNote(AuthSession authSession, string friendId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (friendId == null)
                throw new ArgumentNullException(nameof(friendId));
            if (string.IsNullOrWhiteSpace(friendId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(friendId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/friends/{friendId}/note"));
            await response.Handle();
        }

        public async Task<Settings> GetSettings(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/settings"));
            return await response.Handle<Settings>();
        }

        public async Task<Settings> SetSettings(AuthSession authSession, Settings payload)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.PutAsync(new Uri(_baseUri, $"v1/{authSession.AccountId}/settings"), payload.ToJson());
            return await response.Handle<Settings>();
        }
    }
}