using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FortniteDotNet.Enums.FortniteService;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Models.FortniteService;
using FortniteDotNet.Models.FortniteService.Calendar;
using FortniteDotNet.Models.FortniteService.Content;
using FortniteDotNet.Models.FortniteService.Creative;
using FortniteDotNet.Models.FortniteService.Profile;
using FortniteDotNet.Models.FortniteService.Storefront;

using Profile = FortniteDotNet.Enums.FortniteService.Profile;

namespace FortniteDotNet.Services
{
    public interface IFortniteService
    {
        #region MCP commands

        public Task<Inventory> GetBattleRoyaleInventory(AuthSession authSession);
        
        public Task<McpResponse> CallClientCommand(AuthSession authSession, Command command, object payload, Profile profile = Profile.COMMON_CORE, int revision = -1);
        
        public Task<McpResponse> CallPublicCommand(AuthSession authSession, string accountId, Command command, object payload, Profile profile = Profile.COMMON_CORE, int revision = -1);
        
        #endregion
        
        #region Cloudstorage
        
        public Task<List<CloudstorageFile>> GetSystemCloudstorageFiles(AuthSession authSession);
        public Task<CloudstorageFile> GetSystemCloudstorageFile(AuthSession authSession, string uniqueFilename);
        public Task<List<CloudstorageFile>> GetUserCloudstorageFiles(AuthSession authSession);
        public Task<CloudstorageFile> GetUserCloudstorageFile(AuthSession authSession, string uniqueFilename);
        public Task PutUserCloudstorageFile(AuthSession authSession, string uniqueFilename, byte[] fileBytes);
        public Task DeleteUserCloudstorageFile(AuthSession authSession, string uniqueFilename);
        
        #endregion

        #region Storefront
        
        public Task<List<Receipt>> GetReceipts(AuthSession authSession);
        public Task<Catalog> GetCatalog(AuthSession authSession);
        public Task<List<string>> GetKeychain(AuthSession authSession);

        #endregion
        
        #region Miscellaneous
        
        public Task<AccountPrivacy> GetAccountPrivacy(AuthSession authSession);
        public Task<AccountPrivacy> SetAccountPrivacy(AuthSession authSession, bool optOutOfPublicLeaderboards = false);
        public Task<Pages> GetContentPages();
        public Task<Timeline> GetTimeline(AuthSession authSession);
        public Task<List<ServiceStatus>> GetServiceStatus(AuthSession authSession, params string[] serviceIds);

        #endregion
        
        #region Creative

        public Task<LinkQuery> GetCreativeIslandFavorites(AuthSession authSession, int limit = -1, DateTime? olderThan = null);
        public Task<LinkEntry> AddCreativeIslandToFavorites(AuthSession authSession, string mnemonic);
        public Task RemoveCreativeIslandFromFavorites(AuthSession authSession, string mnemonic);
        
        public Task<LinkQuery> GetCreativeIslandHistory(AuthSession authSession, int limit = -1, DateTime? olderThan = null);
        public Task RemoveCreativeIslandFromHistory(AuthSession authSession, string mnemonic);

        public Task<LinkEntry> LookupLinkByMnemonic(AuthSession authSession, string mnemonic);
        public Task<LinkEntry> LookupLinkByAuthor(AuthSession authSession, string authorId);

        #endregion
    }
    
    public class FortniteService : IFortniteService
    {
        private readonly Uri _baseUri = new Uri("https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/");
        private readonly HttpClient _client = new HttpClient();

        public async Task<Inventory> GetBattleRoyaleInventory(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"game/v2/br-inventory/account/{authSession.AccountId}"));
            return await response.Handle<Inventory>();
        }

        public async Task<McpResponse> CallClientCommand(AuthSession authSession, Command command, object payload, Profile profile, int revision)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"game/v2/profile/{authSession.AccountId}/client/{command}?profileId={profile.ToString().ToLower()}&rvn={revision}"), payload.ToJson());
            return await response.Handle<McpResponse>();
        }

        public async Task<McpResponse> CallPublicCommand(AuthSession authSession, string accountId, Command command, object payload, Profile profile, int revision)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(accountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PostAsync(new Uri(_baseUri, $"game/v2/profile/{accountId}/public/{command}?profileId={profile.ToString().ToLower()}&rvn={revision}"), payload.ToJson());
            return await response.Handle<McpResponse>();
        }
        
        public async Task<List<CloudstorageFile>> GetSystemCloudstorageFiles(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "cloudstorage/system"));
            return await response.Handle<List<CloudstorageFile>>();
        }

        public async Task<CloudstorageFile> GetSystemCloudstorageFile(AuthSession authSession, string uniqueFilename)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"cloudstorage/system/{uniqueFilename}"));
            return await response.Handle<CloudstorageFile>();
        }

        public async Task<List<CloudstorageFile>> GetUserCloudstorageFiles(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"cloudstorage/user/{authSession.AccountId}"));
            return await response.Handle<List<CloudstorageFile>>();
        }

        public async Task<CloudstorageFile> GetUserCloudstorageFile(AuthSession authSession, string uniqueFilename)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"cloudstorage/user/{authSession.AccountId}/{uniqueFilename}"));
            return await response.Handle<CloudstorageFile>();
        }

        public async Task PutUserCloudstorageFile(AuthSession authSession, string uniqueFilename, byte[] fileBytes)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.PutAsync(new Uri(_baseUri, $"cloudstorage/user/{authSession.AccountId}/{uniqueFilename}"), new ByteArrayContent(fileBytes));
            await response.Handle();
        }

        public async Task DeleteUserCloudstorageFile(AuthSession authSession, string uniqueFilename)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.DeleteAsync(new Uri(_baseUri, $"cloudstorage/user/{authSession.AccountId}/{uniqueFilename}"));
            await response.Handle();
        }

        public async Task<List<Receipt>> GetReceipts(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"receipts/v1/account/{authSession.AccountId}/receipts"));
            return await response.Handle<List<Receipt>>();
        }

        public async Task<Catalog> GetCatalog(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "storefront/v2/catalog"));
            return await response.Handle<Catalog>();
        }

        public async Task<List<string>> GetKeychain(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "storefront/v2/keychain"));
            return await response.Handle<List<string>>();
        }

        public async Task<AccountPrivacy> GetAccountPrivacy(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, $"game/v2/privacy/account/{authSession.AccountId}"));
            return await response.Handle<AccountPrivacy>();
        }

        public async Task<AccountPrivacy> SetAccountPrivacy(AuthSession authSession, bool optOutOfPublicLeaderboards)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var content = new AccountPrivacy(authSession.AccountId, optOutOfPublicLeaderboards).ToJson();
            var response = await _client.PostAsync(new Uri(_baseUri, $"game/v2/privacy/account/{authSession.AccountId}"), content);
            return await response.Handle<AccountPrivacy>();
        }

        public async Task<Pages> GetContentPages()
        {
            var response = await _client.GetAsync(new Uri("https://fortnitecontent-website-prod07.ol.epicgames.com/content/api/pages/fortnite-game"));
            return await response.Handle<Pages>();
        }

        public async Task<Timeline> GetTimeline(AuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri(_baseUri, "calendar/v1/timeline"));
            return await response.Handle<Timeline>();
        }

        public async Task<List<ServiceStatus>> GetServiceStatus(AuthSession authSession, params string[] serviceIds)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (serviceIds == null)
                throw new ArgumentNullException(nameof(serviceIds));
            if (serviceIds.Length <= 0)
                throw new ArgumentException("The parameter length must be more than 0.", nameof(serviceIds));

            for (var i = 0; i < serviceIds.Length; i++)
                if (string.IsNullOrWhiteSpace(serviceIds[i]))
                    throw new ArgumentException("The parameter must have some value to it.", $"{nameof(serviceIds)}.{i}");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            var response = await _client.GetAsync(new Uri($"https://lightswitch-public-service-prod.ol.epicgames.com/lightswitch/api/service/bulk/status?serviceId={string.Join("&serviceId=", serviceIds)}"));
            return await response.Handle<List<ServiceStatus>>();
        }

        public async Task<LinkQuery> GetCreativeIslandFavorites(AuthSession authSession, int limit, DateTime? olderThan)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            olderThan ??= DateTime.UtcNow;
            
            var response = await _client.GetAsync(new Uri(_baseUri, $"game/v2/creative/favorites/{authSession.AccountId}?limit={limit}&olderThan={olderThan.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'")}"));
            return await response.Handle<LinkQuery>();
        }

        public async Task<LinkEntry> AddCreativeIslandToFavorites(AuthSession authSession, string mnemonic)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (mnemonic == null)
                throw new ArgumentNullException(nameof(mnemonic));
            if (string.IsNullOrWhiteSpace(mnemonic))
                throw new ArgumentException("The parameter must have some value to it.", nameof(mnemonic));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.PutAsync(new Uri(_baseUri, $"game/v2/creative/favorites/{authSession.AccountId}/{mnemonic}"), null);
            return await response.Handle<LinkEntry>();
        }

        public async Task RemoveCreativeIslandFromFavorites(AuthSession authSession, string mnemonic)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (mnemonic == null)
                throw new ArgumentNullException(nameof(mnemonic));
            if (string.IsNullOrWhiteSpace(mnemonic))
                throw new ArgumentException("The parameter must have some value to it.", nameof(mnemonic));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.DeleteAsync(new Uri(_baseUri, $"game/v2/creative/favorites/{authSession.AccountId}/{mnemonic}"));
            await response.Handle();
        }

        public async Task<LinkQuery> GetCreativeIslandHistory(AuthSession authSession, int limit, DateTime? olderThan)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);

            olderThan ??= DateTime.UtcNow;
            
            var response = await _client.GetAsync(new Uri(_baseUri, $"game/v2/creative/history/{authSession.AccountId}?limit={limit}&olderThan={olderThan.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'")}"));
            return await response.Handle<LinkQuery>();
        }

        public async Task RemoveCreativeIslandFromHistory(AuthSession authSession, string mnemonic)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authSession.AccountId == null)
                throw new ArgumentNullException(nameof(authSession.AccountId));
            if (string.IsNullOrWhiteSpace(authSession.AccountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authSession.AccountId));

            if (mnemonic == null)
                throw new ArgumentNullException(nameof(mnemonic));
            if (string.IsNullOrWhiteSpace(mnemonic))
                throw new ArgumentException("The parameter must have some value to it.", nameof(mnemonic));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.DeleteAsync(new Uri(_baseUri, $"game/v2/creative/favorites/{authSession.AccountId}/{mnemonic}"));
            await response.Handle();
        }

        public async Task<LinkEntry> LookupLinkByMnemonic(AuthSession authSession, string mnemonic)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (mnemonic == null)
                throw new ArgumentNullException(nameof(mnemonic));
            if (string.IsNullOrWhiteSpace(mnemonic))
                throw new ArgumentException("The parameter must have some value to it.", nameof(mnemonic));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.GetAsync(new Uri($"https://links-public-service-live.ol.epicgames.com/links/api/fn/mnemonic/{mnemonic}"));
            return await response.Handle<LinkEntry>();
        }

        public async Task<LinkEntry> LookupLinkByAuthor(AuthSession authSession, string authorId)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            if (authorId == null)
                throw new ArgumentNullException(nameof(authorId));
            if (string.IsNullOrWhiteSpace(authorId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authorId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authSession.AccessToken);
            
            var response = await _client.GetAsync(new Uri($"https://links-public-service-live.ol.epicgames.com/links/api/fn/author/{authorId}"));
            return await response.Handle<LinkEntry>();
        }
    }
}