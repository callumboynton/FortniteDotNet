namespace FortniteDotNet
{
    public struct Endpoints
    {
        public struct Accounts
        {
            private const string BASE_URL = "https://account-public-service-prod.ol.epicgames.com/account";

            public struct OAuth
            {
                public static string Token 
                    => $"{BASE_URL}/api/oauth/token";

                public static string KillSession(string token) 
                    => $"{BASE_URL}/api/oauth/sessions/kill/{token}";

                public static string Exchange 
                    => $"{BASE_URL}/api/oauth/exchange";
            }

            public static string Account(string accountId) 
                => $"{BASE_URL}/api/public/account/{accountId}";

            public static string Account(string[] accountIds) 
                => $"{BASE_URL}/api/public/account?accountId={string.Join("&accountId=", accountIds)}";

            public static string Metadata(string accountId) 
                => $"{BASE_URL}/api/accounts/{accountId}/metadata";

            public static string DeviceAuths(string accountId) 
                => $"{Account(accountId)}/deviceAuths";
            
            public static string DeviceAuth(string accountId, string deviceId) 
                => $"{DeviceAuths(accountId)}/{deviceId}";
            
            public static string ExternalAuths(string accountId) 
                => $"{Account(accountId)}/externalAuths";

            public static string ExternalAuth(string accountId, string type) 
                => $"{ExternalAuths(accountId)}/{type}";

            public static string DisplayNameLookup(string displayName) 
                => $"{BASE_URL}/api/public/account/displayName/{displayName}";

            public static string EmailLookup(string email) 
                => $"{BASE_URL}/api/public/account/email/{email}";
        }

        public struct Channels
        {
            private const string BASE_URL = "https://channels-public-service-prod.ol.epicgames.com";
            
            public static string Setting(string accountId, string setting) 
                => $"{BASE_URL}/api/v1/user/{accountId}/setting/{setting}";

            public static string Available(string accountId, string setting) 
                => $"{Setting(accountId, setting)}/available";

            public static string Setting(string[] accountIds, string setting) 
                => $"{BASE_URL}/api/v1/user/setting/{setting}?accountId={string.Join("&accountId=", accountIds)}";
        }

        public struct Events
        {
            private const string BASE_URL = "https://events-public-service-prod.ol.epicgames.com";

            public static string EventData(string accountId, string query) 
                => $"{BASE_URL}/api/v1/events/Fortnite/download/{accountId}?{query}";

            public static string LeaderboardData(string accountId, string eventId, string eventWindowId) 
                => $"{BASE_URL}/api/v1/leaderboards/Fortnite/{eventId}/{eventWindowId}/{accountId}";
        }

        public struct Fortnite
        {
            private const string BASE_URL = "https://fortnite-public-service-prod11.ol.epicgames.com/fortnite";
            
            public struct Mcp
            {
                private static string ClientCommand(string accountId, string command, string query) 
                    => $"{BASE_URL}/api/game/v2/profile/{accountId}/client/{command}?{query}";

                public static string QueryProfile(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "QueryProfile", $"profileId={profileId}&rvn={revision}");

                public static string ClientQuestLogin(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "ClientQuestLogin", $"profileId={profileId}&rvn={revision}");

                public static string MarkItemSeen(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "MarkItemSeen", $"profileId={profileId}&rvn={revision}");

                public static string GiftCatalogEntry(string accountId, int revision)
                    => ClientCommand(accountId, "GiftCatalogEntry", $"profileId=common_core&rvn={revision}");
            }

            public struct Storefront
            {
                public static string Catalog
                    => $"{BASE_URL}/api/storefront/v2/catalog";

                public static string Keychain
                    => $"{BASE_URL}/api/storefront/v2/keychain";
            }

            public struct Cloudstorage
            {
                public static string System
                    => $"{BASE_URL}/api/cloudstorage/system";

                public static string SystemFile(string uniqueFileName)
                    => $"{System}/{uniqueFileName}";

                public static string User(string id)
                    => $"{BASE_URL}/api/cloudstorage/user/{id}";

                public static string UserFile(string id, string uniqueFileName)
                    => $"{User(id)}/{uniqueFileName}";
            }
            
            public static string AccountPrivacy(string accountId)
                => $"{BASE_URL}/api/game/v2/privacy/account/{accountId}";
            
            public static string Timeline
                => $"{BASE_URL}/api/calendar/v1/timeline";
            
            
        }
    }
}