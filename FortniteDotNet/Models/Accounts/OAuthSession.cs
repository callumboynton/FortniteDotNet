using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Enums.Events;
using FortniteDotNet.Models.Events;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Enums.Channels;
using FortniteDotNet.Models.Channels;
using FortniteDotNet.Models.Fortnite;
using FortniteDotNet.Payloads.Channels;
using FortniteDotNet.Payloads.Fortnite;

namespace FortniteDotNet.Models.Accounts
{
    public class OAuthSession : IAsyncDisposable
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("refresh_expires")]
        public int RefreshExpires { get; set; }

        [JsonProperty("refresh_expires_at")]
        public DateTime RefreshExpiresAt { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("internal_client")]
        public bool InternalClient { get; set; }

        [JsonProperty("client_service")]
        public string ClientService { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        
        [JsonProperty("perms")]
        public List<Permission> Permissions { get; set; }
        
        [JsonProperty("app")] 
        public string App { get; set; }

        [JsonProperty("in_app_id")]
        public string InAppId { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("scope")]
        public List<string> Scope { get; set; }

        #region AccountService
        
        /// <inheritdoc cref="AccountService.KillOAuthSession"/>
        public async Task KillSessionAsync()
            => await AccountService.KillOAuthSession(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GenerateExchangeCode"/>
        public async Task<ExchangeCode> GenerateExchangeCodeAsync()
            => await AccountService.GenerateExchangeCode(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetInformation(OAuthSession)"/>
        public async Task<AccountInfo> GetAccountInfoAsync()
            => await AccountService.GetInformation(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetDeviceAuths"/>
        public async Task<List<DeviceAuth>> GetDeviceAuthsAsync()
            => await AccountService.GetDeviceAuths(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetDeviceAuth"/>
        public async Task<DeviceAuth> GetDeviceAuthAsync(string deviceId)
            => await AccountService.GetDeviceAuth(this, deviceId).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.CreateDeviceAuth"/>
        public async Task<DeviceAuth> CreateDeviceAuthAsync()
            => await AccountService.CreateDeviceAuth(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.DeleteDeviceAuth"/>
        public async Task DeleteDeviceAuthAsync(string deviceId)
            => await AccountService.DeleteDeviceAuth(this, deviceId).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetExternalAuths"/>
        public async Task<List<ExternalAuth>> GetExternalAuthsAsync()
            => await AccountService.GetExternalAuths(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetExternalAuth"/>
        public async Task<ExternalAuth> GetExternalAuthAsync(string type)
            => await AccountService.GetExternalAuth(this, type).ConfigureAwait(false);
        
        /*
            
            /// <inheritdoc cref="AccountService.AddExternalAuth"/>
            public async Task<object> AddExternalAuthAsync(AddExternalAuth payload)
                => await AccountService.AddExternalAuth(this, payload).ConfigureAwait(false);
        */
        
        /// <inheritdoc cref="AccountService.DeleteExternalAuth"/>
        public async Task DeleteExternalAuthAsync(string type)
            => await AccountService.DeleteExternalAuth(this, type).ConfigureAwait(false);

        #endregion
        
        #region ChannelsService
        
        /// <inheritdoc cref="ChannelsService.GetUserSetting"/>
        public async Task<UserSetting> GetUserSettingAsync(SettingKey settingKey)
            => await ChannelsService.GetUserSetting(this, settingKey);

        /// <inheritdoc cref="ChannelsService.UpdateUserSetting"/>
        public async Task UpdateUserSettingAsync(SettingKey settingKey, UpdateUserSetting payload)
            => await ChannelsService.UpdateUserSetting(this, settingKey, payload);

        /// <inheritdoc cref="ChannelsService.GetAvailableSettingValues"/>
        public async Task<List<string>> GetAvailableSettingValuesAsync(SettingKey settingKey)
            => await ChannelsService.GetAvailableSettingValues(this, settingKey);
        
        #endregion
        
        #region EventsService
        
        /// <inheritdoc cref="EventsService.GetEventData"/>
        public async Task<EventData> GetEventDataAsync(Region region, Platform platform)
            => await EventsService.GetEventData(this, region, platform);
        
        /// <inheritdoc cref="EventsService.GetEventData"/>
        public async Task<Leaderboard> GetLeaderboardDataAsync(string eventId, string eventWindowId)
            => await EventsService.GetLeaderboardData(this, eventId, eventWindowId);
        
        #endregion
        
        #region FortniteService

        /// <inheritdoc cref="FortniteService.QueryProfile"/>
        public async Task<McpResponse> QueryProfile(Profile profile, int revision = -1)
            => await FortniteService.QueryProfile(this, profile, revision);
        
        /// <inheritdoc cref="FortniteService.ClientQuestLogin"/>
        public async Task<McpResponse> ClientQuestLogin(Profile profile, int revision = -1)
            => await FortniteService.ClientQuestLogin(this, profile, revision);
        
        /// <inheritdoc cref="FortniteService.MarkItemSeen"/>
        public async Task<McpResponse> MarkItemSeen(Profile profile, MarkItemSeen payload, int revision = -1)
            => await FortniteService.MarkItemSeen(this, profile, payload, revision);
        
        /// <inheritdoc cref="FortniteService.GetAccountPrivacy"/>
        public async Task<AccountPrivacy> GetAccountPrivacy()
            => await FortniteService.GetAccountPrivacy(this);
        
        #endregion
        
        public async ValueTask DisposeAsync()
        {
            await KillSessionAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}