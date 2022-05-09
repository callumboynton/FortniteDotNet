using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FortniteDotNet.Enums.AccountService;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Payloads.AccountService;

namespace FortniteDotNet.Services
{
    public interface IAccountService
    {
        #region Authentication

        public Task<AuthSession> CreateAuthSession(AuthClient authClient, GrantType grantType, bool includePerms = false);
        public Task<VerifiedAuthSession> VerifyAuthSession(AuthSession authSession);
        public Task KillAuthSession(AuthSession authSession, string sessionId);
        public Task KillAuthSessions(AuthSession authSession, KillType killType = KillType.OTHERS_ACCOUNT_CLIENT_SERVICE);

        #endregion
        
        #region External Auths
        
        public Task<List<ExternalAuth>> GetExternalAuths(AuthSession authSession);
        public Task<ExternalAuth> CreateExternalAuth(AuthSession authSession, CreateExternalAuth payload);
        public Task<ExternalAuth> GetExternalAuth(AuthSession authSession, string type);
        public Task DeleteExternalAuth(AuthSession authSession, string type);
        
        #endregion

        #region Device Auths

        public Task<List<DeviceAuth>> GetDeviceAuths(AuthSession authSession);
        public Task<DeviceAuth> CreateDeviceAuth(AuthSession authSession);
        public Task<DeviceAuth> GetDeviceAuth(AuthSession authSession, string deviceId);
        public Task DeleteDeviceAuth(AuthSession authSession, string deviceId);
        
        #endregion
        
        #region Lookup

        public Task<Dictionary<string, string>> GetMetadata(AuthSession authSession);
        public Task<AccountInfo> LookupById(AuthSession authSession, string accountId);
        public Task<List<AccountInfo>> LookupByIds(AuthSession authSession, params string[] accountIds);
        public Task<AccountInfo> LookupByEmail(AuthSession authSession, string email);
        public Task<AccountInfo> LookupByDisplayName(AuthSession authSession, string displayName);

        #endregion
    }

    public class AccountService : IAccountService
    {
        private readonly Uri _baseUri = new Uri("https://account-public-service-prod.ol.epicgames.com/account/api/");
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<AuthSession> CreateAuthSession(AuthClient authClient, GrantType grantType, bool includePerms)
        {
            if (authClient == null)
                throw new ArgumentNullException(nameof(authClient));

            if (authClient.Token == null)
                throw new ArgumentNullException(nameof(authClient.Token));
            if (string.IsNullOrWhiteSpace(authClient.Token))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authClient.Token));

            if (grantType == null)
                throw new ArgumentNullException(nameof(grantType));

            var nameValueCollection = new List<KeyValuePair<string, string>>();
            foreach (var nameValuePair in grantType.ToString().Split('&'))
            {
                var parts = nameValuePair.Split('=');
                nameValueCollection.Add(new KeyValuePair<string, string>(parts[0], parts[1]));
            }

            nameValueCollection.Add(new KeyValuePair<string, string>("includePerms", includePerms.ToString().ToLower()));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", authClient.Token);

            var content = new FormUrlEncodedContent(nameValueCollection);
            var response = await _client.PostAsync(new Uri(_baseUri, "oauth/token"), content);
            return await response.Handle<AuthSession>();
        }

        public async Task<VerifiedAuthSession> VerifyAuthSession(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "oauth/verify"));
            return await response.Handle<VerifiedAuthSession>();
        }

        public async Task KillAuthSession(AuthSession authSession, string sessionId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (sessionId == null)
                throw new ArgumentNullException(nameof(sessionId));
            if (string.IsNullOrWhiteSpace(sessionId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(sessionId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"oauth/sessions/kill/{sessionId}"));
            await response.Handle();
        }

        public async Task KillAuthSessions(AuthSession authSession, KillType killType)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (!Enum.IsDefined(typeof(KillType), killType))
                throw new ArgumentException("The parameter must have a valid value (out of range).", nameof(killType));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"oauth/sessions/kill?killType={killType.ToString()}"));
            await response.Handle();
        }

        public async Task<List<ExternalAuth>> GetExternalAuths(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/externalAuths"));
            return await response.Handle<List<ExternalAuth>>();
        }

        public async Task<ExternalAuth> CreateExternalAuth(AuthSession authSession, CreateExternalAuth payload)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/deviceAuth"), payload.ToJson());
            return await response.Handle<ExternalAuth>();
        }

        public async Task<ExternalAuth> GetExternalAuth(AuthSession authSession, string type)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("The parameter must have some value to it.", nameof(type));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/externalAuths/{type}"));
            return await response.Handle<ExternalAuth>();
        }

        public async Task DeleteExternalAuth(AuthSession authSession, string type)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("The parameter must have some value to it.", nameof(type));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/externalAuths/{type}"));
            await response.Handle();
        }

        public async Task<List<DeviceAuth>> GetDeviceAuths(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/deviceAuth"));
            return await response.Handle<List<DeviceAuth>>();
        }

        public async Task<DeviceAuth> CreateDeviceAuth(AuthSession authSession)
        {            
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/deviceAuth"), null);
            return await response.Handle<DeviceAuth>();
        }

        public async Task<DeviceAuth> GetDeviceAuth(AuthSession authSession, string deviceId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (deviceId == null)
                throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(deviceId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/deviceAuth/{deviceId}"));
            return await response.Handle<DeviceAuth>();
        }

        public async Task DeleteDeviceAuth(AuthSession authSession, string deviceId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (deviceId == null)
                throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(deviceId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"public/account/{authSession.AccountId}/deviceAuth/{deviceId}"));
            await response.Handle();
        }

        public async Task<Dictionary<string, string>> GetMetadata(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"accounts/{authSession.AccountId}/metadata"));
            return await response.Handle<Dictionary<string, string>>();
        }

        public async Task<AccountInfo> LookupById(AuthSession authSession, string accountId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(accountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/{accountId}"));
            return await response.Handle<AccountInfo>();
        }

        public async Task<List<AccountInfo>> LookupByIds(AuthSession authSession, params string[] accountIds)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (accountIds == null)
                throw new ArgumentNullException(nameof(accountIds));
            if (accountIds.Length <= 0 || accountIds.Length > 100)
                throw new ArgumentException("The parameter length must be more than 0 and less than 100.", nameof(accountIds));

            for (var i = 0; i < accountIds.Length; i++)
                if (string.IsNullOrWhiteSpace(accountIds[i]))
                    throw new ArgumentException("The parameter must have some value to it.", $"{nameof(accountIds)}.{i}");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account?accountId={string.Join("&accountId=", accountIds)}"));
            return await response.Handle<List<AccountInfo>>();
        }

        public async Task<AccountInfo> LookupByEmail(AuthSession authSession, string email)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (email == null)
                throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The parameter must have some value to it.", nameof(email));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/email/{email}"));
            return await response.Handle<AccountInfo>();
        }

        public async Task<AccountInfo> LookupByDisplayName(AuthSession authSession, string displayName)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (displayName == null)
                throw new ArgumentNullException(nameof(displayName));
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("The parameter must have some value to it.", nameof(displayName));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"public/account/displayName/{displayName}"));
            return await response.Handle<AccountInfo>();
        }

        public async Task<ExchangeSession> CreateExchangeSession(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "oauth/exchange"));
            return await response.Handle<ExchangeSession>();
        }
    }
}