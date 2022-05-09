using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.AccountService
{
    public class AuthSession
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        [JsonProperty("expires_in")] 
        public int ExpiresIn { get; internal set; }

        [JsonProperty("expires_at")] 
        public DateTime ExpiresAt { get; internal set; }

        [JsonIgnore] 
        public bool HasSessionExpired => DateTime.UtcNow.CompareTo(ExpiresAt) > 0;

        [JsonProperty("token_type")] 
        public string TokenType { get; internal set; }

        [JsonProperty("refresh_token")] 
        public string RefreshToken { get; internal set; }

        [JsonProperty("refresh_expires")] 
        public int RefreshExpires { get; internal set; }

        [JsonProperty("refresh_expires_at")] 
        public DateTime RefreshExpiresAt { get; internal set; }

        [JsonIgnore] 
        public bool HasRefreshExpired => DateTime.UtcNow.CompareTo(RefreshExpiresAt) > 0;

        [JsonProperty("account_id")] 
        public string AccountId { get; internal set; }

        [JsonProperty("client_id")] 
        public string ClientId { get; internal set; }

        [JsonProperty("internal_client")] 
        public bool InternalClient { get; internal set; }

        [JsonProperty("client_service")] 
        public string ClientService { get; internal set; }

        [JsonProperty("perms")] 
        public List<Permission> Permissions { get; internal set; }

        [JsonProperty("displayName")] 
        public string DisplayName { get; internal set; }

        [JsonProperty("app")] 
        public string App { get; internal set; }

        [JsonProperty("in_app_id")] 
        public string InAppId { get; internal set; }

        [JsonProperty("device_id")] 
        public string DeviceId { get; internal set; }

        [JsonProperty("scope")] 
        public List<string> Scope { get; internal set; }

        [JsonConstructor]
        internal AuthSession()
        {
        }
    }

    public class Permission
    {
        [JsonProperty("resource")] 
        public string Resource { get; internal set; }

        [JsonProperty("action")] 
        public Action Action { get; internal set; }
    }

    public enum Action
    {
        None = 0,

        Create = 1,
        CreateRead = Create + Read,
        CreateUpdate = Create + Update,
        CreateDelete = Create + Delete,
        CreateReadUpdate = Create + Read + Update,
        CreateReadDelete = Create + Read + Delete,
        CreateUpdateDelete = Create + Update + Delete,

        Read = 2,
        ReadUpdate = Read + Update,
        ReadDelete = Read + Delete,
        ReadUpdateDelete = Read + Update + Delete,

        Update = 4,
        UpdateDelete = Update + Delete,

        Delete = 8,

        All = Create + Read + Update + Delete,

        Deny = 16
    }
}