using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using FortniteDotNet.Models.XMPP;
using System.Collections.Generic;
using FortniteDotNet.Enums.Events;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.Events;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Enums.Channels;
using FortniteDotNet.Models.Friends;
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
        public async Task KillSession()
            => await AccountService.KillOAuthSession(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GenerateExchangeCode"/>
        public async Task<ExchangeCode> GenerateExchangeCode()
            => await AccountService.GenerateExchangeCode(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetInformation(OAuthSession)"/>
        public async Task<AccountInfo> GetAccountInfo()
            => await AccountService.GetInformation(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetDeviceAuths"/>
        public async Task<List<DeviceAuth>> GetDeviceAuths()
            => await AccountService.GetDeviceAuths(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetDeviceAuth"/>
        public async Task<DeviceAuth> GetDeviceAuth(string deviceId)
            => await AccountService.GetDeviceAuth(this, deviceId).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.CreateDeviceAuth"/>
        public async Task<DeviceAuth> CreateDeviceAuth()
            => await AccountService.CreateDeviceAuth(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.DeleteDeviceAuth"/>
        public async Task DeleteDeviceAuth(string deviceId)
            => await AccountService.DeleteDeviceAuth(this, deviceId).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetExternalAuths"/>
        public async Task<List<ExternalAuth>> GetExternalAuths()
            => await AccountService.GetExternalAuths(this).ConfigureAwait(false);
        
        /// <inheritdoc cref="AccountService.GetExternalAuth"/>
        public async Task<ExternalAuth> GetExternalAuth(string type)
            => await AccountService.GetExternalAuth(this, type).ConfigureAwait(false);
        
        /*
            
            /// <inheritdoc cref="AccountService.AddExternalAuth"/>
            public async Task<object> AddExternalAuth(AddExternalAuth payload)
                => await AccountService.AddExternalAuth(this, payload).ConfigureAwait(false);
        */
        
        /// <inheritdoc cref="AccountService.DeleteExternalAuth"/>
        public async Task DeleteExternalAuth(string type)
            => await AccountService.DeleteExternalAuth(this, type).ConfigureAwait(false);

        #endregion
        
        #region ChannelsService
        
        /// <inheritdoc cref="ChannelsService.GetUserSetting"/>
        public async Task<UserSetting> GetUserSetting(SettingKey settingKey)
            => await ChannelsService.GetUserSetting(this, settingKey);

        /// <inheritdoc cref="ChannelsService.UpdateUserSetting"/>
        public async Task UpdateUserSetting(SettingKey settingKey, UpdateUserSetting payload)
            => await ChannelsService.UpdateUserSetting(this, settingKey, payload);

        /// <inheritdoc cref="ChannelsService.GetAvailableSettingValues"/>
        public async Task<List<string>> GetAvailableSettingValues(SettingKey settingKey)
            => await ChannelsService.GetAvailableSettingValues(this, settingKey);
        
        #endregion
        
        #region EventsService
        
        /// <inheritdoc cref="EventsService.GetEventData"/>
        public async Task<EventData> GetEventData(Region region, Platform platform)
            => await EventsService.GetEventData(this, region, platform);
        
        /// <inheritdoc cref="EventsService.GetEventData"/>
        public async Task<Leaderboard> GetLeaderboardData(string eventId, string eventWindowId)
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

        /// <inheritdoc cref="FortniteService.GiftCatalogEntry"/>
        [Obsolete("Using this profile command directly may result in gifts being revoked, so proceed with caution!")]
        public async Task<McpResponse> GiftCatalogEntry(GiftCatalogEntry payload, int revision = -1)
            => await FortniteService.GiftCatalogEntry(this, payload, revision);
        
        /// <inheritdoc cref="FortniteService.GetAccountPrivacy"/>
        public async Task<AccountPrivacy> GetAccountPrivacy()
            => await FortniteService.GetAccountPrivacy(this);
        
        /// <inheritdoc cref="FortniteService.SetAccountPrivacy"/>
        public async Task<AccountPrivacy> SetAccountPrivacy(bool optOutOfPublicLeaderboards)
            => await FortniteService.SetAccountPrivacy(this, optOutOfPublicLeaderboards);

        /// <inheritdoc cref="FortniteService.GetUserCloudstorageFiles"/>
        public async Task<List<CloudstorageFile>> GetUserCloudstorageFiles()
            => await FortniteService.GetUserCloudstorageFiles(this);

        /// <inheritdoc cref="FortniteService.GetUserCloudstorageFile"/>
        public async Task<List<CloudstorageFile>> GetUserCloudstorageFile(string uniqueFilename)
            => await FortniteService.GetUserCloudstorageFile(this, uniqueFilename);

        /// <inheritdoc cref="FortniteService.PutUserCloudstorageFile"/>
        public async Task PutUserCloudstorageFile(string uniqueFilename, byte[] fileBytes)
            => await FortniteService.PutUserCloudstorageFile(this, uniqueFilename, fileBytes);

        /// <inheritdoc cref="FortniteService.GetReceipts"/>
        public async Task<List<Receipt>> GetReceipts()
            => await FortniteService.GetReceipts(this);

        #endregion
        
        #region FriendsService
        
        /// <inheritdoc cref="FriendsService.GetSummary"/>
        public async Task<Summary> GetSummary()
            => await FriendsService.GetSummary(this);

        /// <inheritdoc cref="FriendsService.GetAcceptedFriends"/>
        public async Task<List<Friend>> GetAcceptedFriends()
            => await FriendsService.GetAcceptedFriends(this);

        /// <inheritdoc cref="FriendsService.GetFriend"/>
        public async Task<Friend> GetFriend(string friendId)
            => await FriendsService.GetFriend(this, friendId);

        /// <inheritdoc cref="FriendsService.SendOrAcceptFriendRequest"/>
        public async Task SendOrAcceptFriendRequest(string friendId)
            => await FriendsService.SendOrAcceptFriendRequest(this, friendId);

        /// <inheritdoc cref="FriendsService.DeleteFriendOrRequest"/>
        public async Task DeleteFriendOrRequest(string friendId)
            => await FriendsService.DeleteFriendOrRequest(this, friendId);

        /// <inheritdoc cref="FriendsService.UpdateFriendAlias"/>
        public async Task UpdateFriendAlias(string friendId, string newAlias)
            => await FriendsService.UpdateFriendAlias(this, friendId, newAlias);

        /// <inheritdoc cref="FriendsService.RemoveFriendAlias"/>
        public async Task RemoveFriendAlias(string friendId)
            => await FriendsService.RemoveFriendAlias(this, friendId);

        /// <inheritdoc cref="FriendsService.UpdateFriendNote"/>
        public async Task UpdateFriendNote(string friendId, string newNote)
            => await FriendsService.UpdateFriendNote(this, friendId, newNote);

        /// <inheritdoc cref="FriendsService.RemoveFriendNote"/>
        public async Task RemoveFriendNote(string friendId)
            => await FriendsService.RemoveFriendNote(this, friendId);

        /// <inheritdoc cref="FriendsService.GetIncomingFriendRequests"/>
        public async Task<List<Friend>> GetIncomingFriendRequests()
            => await FriendsService.GetIncomingFriendRequests(this);

        /// <inheritdoc cref="FriendsService.GetOutgoingFriendRequests"/>
        public async Task<List<Friend>> GetOutgoingFriendRequests()
            => await FriendsService.GetOutgoingFriendRequests(this);

        /// <inheritdoc cref="FriendsService.GetBlocklist"/>
        public async Task<List<BlockedFriend>> GetBlocklist()
            => await FriendsService.GetBlocklist(this);

        /// <inheritdoc cref="FriendsService.BlockFriend"/>
        public async Task BlockFriend(string friendId)
            => await FriendsService.BlockFriend(this, friendId);

        /// <inheritdoc cref="FriendsService.UnblockFriend"/>
        public async Task UnblockFriend(string friendId)
            => await FriendsService.UnblockFriend(this, friendId);

        /// <inheritdoc cref="FriendsService.UpdateFriendSettings"/>
        public async Task UpdateFriendSettings(bool acceptInvites)
            => await FriendsService.UpdateFriendSettings(this, acceptInvites);
        
        #endregion
        
        #region PartyService

        /// <inheritdoc cref="PartyService.CreateParty"/>
        public async Task<PartyInfo> CreateParty(XMPPClient xmppClient)
            => await PartyService.CreateParty(this, xmppClient);
        
        /// <inheritdoc cref="PartyService.GetPartyPings"/>
        public async Task<List<PartyInfo>> GetPartyPings(string pingerId)
            => await PartyService.GetPartyPings(this, pingerId);
        
        /// <inheritdoc cref="PartyService.GetParty"/>
        public async Task<PartyInfo> GetParty(string partyId)
            => await PartyService.GetParty(this, partyId);
        
        /// <inheritdoc cref="PartyService.JoinParty"/>
        public async Task JoinParty(XMPPClient xmppClient, PartyInvite partyInvite)
            => await PartyService.JoinParty(this, xmppClient, partyInvite);
        
        #endregion
        
        public async ValueTask DisposeAsync()
        {
            await KillSession().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}