using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Models.PartyService;

namespace FortniteDotNet.Services
{
    public interface IPartyService
    {
        #region User
        
        public Task<Summary> GetSummary(AuthSession authSession);
        public Task<List<Ping>> GetPings(AuthSession authSession);
        public Task<List<Party>> GetCurrentParties(AuthSession authSession);
        
        #endregion

        #region Pings
        
        public Task<Ping> SendPing(AuthSession authSession, string pingeeId, Dictionary<string, string> payload = null);
        public Task DeletePing(AuthSession authSession, string pingerId);
        public Task<JoinStatus> JoinPartyByPinger(AuthSession authSession, string pingerId);
        public Task<List<Party>> GetPartiesByPinger(AuthSession authSession, string pingerId);
        
        #endregion

        #region Parties
        
        public Task<Party> GetParty(AuthSession authSession, string partyId);
        public Task UpdateParty(AuthSession authSession, string partyId, PartyUpdate payload);
        public Task DeleteParty(AuthSession authSession, string partyId);

        public Task<Party> CreateParty(AuthSession authSession, CreationInfo payload);
        public Task<JoinStatus> JoinParty(AuthSession authSession, string partyId);
        
        #endregion
        
        #region Members
        
        public Task RemoveMember(AuthSession authSession, string partyId, string memberId);
        public Task UpdateMember(AuthSession authSession, string partyId, string memberId, MemberUpdate payload);
        public Task PromoteMember(AuthSession authSession, string partyId, string memberId);
        public Task<JoinStatus> ConfirmMember(AuthSession authSession, string partyId, string memberId);
        
        #endregion

        #region Invites
        
        public Task SendInvite(AuthSession authSession, string partyId, string inviteeId, bool sendPing = true);
        public Task DeclineInvite(AuthSession authSession, string partyId, string inviterId);
        public Task RemoveInvite(AuthSession authSession, string partyId, string inviteeId);
        
        #endregion
    }
    
    public class PartyService : IPartyService
    {
        private readonly Uri _baseUri = new Uri("https://party-service-prod.ol.epicgames.com/party/api/v1/Fortnite/");
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<Summary> GetSummary(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"user/{authSession.AccountId}"));
            return await response.Handle<Summary>();
        }

        public async Task<List<Ping>> GetPings(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"user/{authSession.AccountId}/pings"));
            return await response.Handle<List<Ping>>();
        }

        public async Task<List<Party>> GetCurrentParties(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"user/{authSession.AccountId}/current"));
            return await response.Handle<List<Party>>();
        }

        public async Task<Ping> SendPing(AuthSession authSession, string pingeeId, Dictionary<string, string> payload)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (pingeeId == null)
                throw new ArgumentNullException(nameof(pingeeId));
            if (string.IsNullOrWhiteSpace(pingeeId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(pingeeId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"user/{pingeeId}/pings/{authSession.AccountId}"), payload?.ToJson());
            return await response.Handle<Ping>();
        }

        public async Task DeletePing(AuthSession authSession, string pingerId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (pingerId == null)
                throw new ArgumentNullException(nameof(pingerId));
            if (string.IsNullOrWhiteSpace(pingerId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(pingerId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"user/{authSession.AccountId}/pings/{pingerId}"));
            await response.Handle();
        }

        public async Task<JoinStatus> JoinPartyByPinger(AuthSession authSession, string pingerId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (pingerId == null)
                throw new ArgumentNullException(nameof(pingerId));
            if (string.IsNullOrWhiteSpace(pingerId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(pingerId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"user/{authSession.AccountId}/pings/{pingerId}/join"), null);
            return await response.Handle<JoinStatus>();
        }

        public async Task<List<Party>> GetPartiesByPinger(AuthSession authSession, string pingerId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (pingerId == null)
                throw new ArgumentNullException(nameof(pingerId));
            if (string.IsNullOrWhiteSpace(pingerId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(pingerId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"user/{authSession.AccountId}/pings/{pingerId}/parties"));
            return await response.Handle<List<Party>>();
        }

        public async Task<Party> GetParty(AuthSession authSession, string partyId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"parties/{partyId}"));
            return await response.Handle<Party>();
        }

        public async Task UpdateParty(AuthSession authSession, string partyId, PartyUpdate payload)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PatchAsync(new Uri(_baseUri, $"parties/{partyId}"), payload.ToJson());
            await response.Handle();
        }

        public async Task DeleteParty(AuthSession authSession, string partyId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"parties/{partyId}"));
            await response.Handle();
        }

        public async Task<Party> CreateParty(AuthSession authSession, CreationInfo payload)
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

            var response = await _client.PostAsync(new Uri(_baseUri, "parties"), payload.ToJson());
            return await response.Handle<Party>();
        }

        public async Task<JoinStatus> JoinParty(AuthSession authSession, string partyId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"parties/{partyId}/join"), null);
            return await response.Handle<JoinStatus>();
        }

        public async Task RemoveMember(AuthSession authSession, string partyId, string memberId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (memberId == null)
                throw new ArgumentNullException(nameof(memberId));
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(memberId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"parties/{partyId}/members/{memberId}"));
            await response.Handle();
        }

        public async Task UpdateMember(AuthSession authSession, string partyId, string memberId, MemberUpdate payload)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (memberId == null)
                throw new ArgumentNullException(nameof(memberId));
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(memberId));

            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PatchAsync(new Uri(_baseUri, $"parties/{partyId}/members/{memberId}/meta"), payload.ToJson());
            await response.Handle();
        }

        public async Task PromoteMember(AuthSession authSession, string partyId, string memberId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (memberId == null)
                throw new ArgumentNullException(nameof(memberId));
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(memberId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"parties/{partyId}/members/{memberId}/promote"), null);
            await response.Handle();
        }

        public async Task<JoinStatus> ConfirmMember(AuthSession authSession, string partyId, string memberId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (memberId == null)
                throw new ArgumentNullException(nameof(memberId));
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(memberId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"parties/{partyId}/members/{memberId}/confirm"), null);
            return await response.Handle<JoinStatus>();
        }

        public async Task SendInvite(AuthSession authSession, string partyId, string inviteeId, bool sendPing)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (inviteeId == null)
                throw new ArgumentNullException(nameof(inviteeId));
            if (string.IsNullOrWhiteSpace(inviteeId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(inviteeId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"parties/{partyId}/invites/{inviteeId}?sendPing={sendPing}"), null);
            await response.Handle();
        }

        public async Task DeclineInvite(AuthSession authSession, string partyId, string inviterId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (inviterId == null)
                throw new ArgumentNullException(nameof(inviterId));
            if (string.IsNullOrWhiteSpace(inviterId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(inviterId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"parties/{partyId}/invites/{inviterId}"));
            await response.Handle();
        }

        public async Task RemoveInvite(AuthSession authSession, string partyId, string inviteeId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (partyId == null)
                throw new ArgumentNullException(nameof(partyId));
            if (string.IsNullOrWhiteSpace(partyId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(partyId));

            if (inviteeId == null)
                throw new ArgumentNullException(nameof(inviteeId));
            if (string.IsNullOrWhiteSpace(inviteeId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(inviteeId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"parties/{partyId}/invites/{inviteeId}"));
            await response.Handle();
        }
    }
}