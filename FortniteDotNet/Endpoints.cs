namespace FortniteDotNet
{
    internal struct Endpoints
    {
        internal struct Accounts
        {
            private const string BASE_URL = "https://account-public-service-prod.ol.epicgames.com/account";

            internal struct OAuth
            {
                internal static string Token 
                    => $"{BASE_URL}/api/oauth/token";

                internal static string KillSession(string token) 
                    => $"{BASE_URL}/api/oauth/sessions/kill/{token}";

                internal static string Exchange 
                    => $"{BASE_URL}/api/oauth/exchange";
            }

            internal static string Account(string accountId) 
                => $"{BASE_URL}/api/public/account/{accountId}";

            internal static string Account(string[] accountIds) 
                => $"{BASE_URL}/api/public/account?accountId={string.Join("&accountId=", accountIds)}";

            internal static string Metadata(string accountId) 
                => $"{BASE_URL}/api/public/{accountId}/metadata";

            internal static string DeviceAuths(string accountId) 
                => $"{Account(accountId)}/deviceAuths";
            
            internal static string DeviceAuth(string accountId, string deviceId) 
                => $"{DeviceAuths(accountId)}/{deviceId}";
            
            internal static string ExternalAuths(string accountId) 
                => $"{Account(accountId)}/externalAuths";

            internal static string ExternalAuth(string accountId, string type) 
                => $"{ExternalAuths(accountId)}/{type}";

            internal static string DisplayNameLookup(string displayName) 
                => $"{BASE_URL}/api/public/account/displayName/{displayName}";

            internal static string EmailLookup(string email) 
                => $"{BASE_URL}/api/public/account/email/{email}";
        }

        internal struct Channels
        {
            private const string BASE_URL = "https://channels-public-service-prod.ol.epicgames.com";
            
            internal static string Setting(string accountId, string setting) 
                => $"{BASE_URL}/api/v1/user/{accountId}/setting/{setting}";

            internal static string Available(string accountId, string setting) 
                => $"{Setting(accountId, setting)}/available";

            internal static string Setting(string[] accountIds, string setting) 
                => $"{BASE_URL}/api/v1/user/setting/{setting}?accountId={string.Join("&accountId=", accountIds)}";
        }

        internal struct Events
        {
            private const string BASE_URL = "https://events-public-service-prod.ol.epicgames.com";

            internal static string EventData(string accountId, string query) 
                => $"{BASE_URL}/api/v1/events/Fortnite/download/{accountId}?{query}";

            internal static string LeaderboardData(string accountId, string eventId, string eventWindowId) 
                => $"{BASE_URL}/api/v1/leaderboards/Fortnite/{eventId}/{eventWindowId}/{accountId}";
        }

        internal struct Fortnite
        {
            private const string BASE_URL = "https://fortnite-public-service-prod11.ol.epicgames.com/fortnite";
            
            internal struct Mcp
            {
                private static string ClientCommand(string accountId, string command, string query) 
                    => $"{BASE_URL}/api/game/v2/profile/{accountId}/client/{command}?{query}";

                internal static string QueryProfile(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "QueryProfile", $"profileId={profileId}&rvn={revision}");

                internal static string ClientQuestLogin(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "ClientQuestLogin", $"profileId={profileId}&rvn={revision}");

                internal static string MarkItemSeen(string accountId, string profileId, int revision)
                    => ClientCommand(accountId, "MarkItemSeen", $"profileId={profileId}&rvn={revision}");

                internal static string GiftCatalogEntry(string accountId, int revision)
                    => ClientCommand(accountId, "GiftCatalogEntry", $"profileId=common_core&rvn={revision}");
            }

            internal struct Storefront
            {
                internal static string Catalog
                    => $"{BASE_URL}/api/storefront/v2/catalog";

                internal static string Keychain
                    => $"{BASE_URL}/api/storefront/v2/keychain";
            }

            internal struct Cloudstorage
            {
                internal static string System
                    => $"{BASE_URL}/api/cloudstorage/system";

                internal static string SystemFile(string uniqueFileName)
                    => $"{System}/{uniqueFileName}";

                internal static string User(string id)
                    => $"{BASE_URL}/api/cloudstorage/user/{id}";

                internal static string UserFile(string id, string uniqueFileName)
                    => $"{User(id)}/{uniqueFileName}";
            }

            internal struct Creative
            {
                internal static string Favorites(string accountId)
                    => $"{BASE_URL}/api/game/v2/creative/favorites/{accountId}";
                
                internal static string Favorites(string accountId, string islandCode)
                    => $"{BASE_URL}/api/game/v2/creative/favorites/{accountId}/{islandCode}";

                internal static string History(string accountId)
                    => $"{BASE_URL}/api/game/v2/creative/history/{accountId}";
            }
            
            internal static string AccountPrivacy(string accountId)
                => $"{BASE_URL}/api/game/v2/privacy/account/{accountId}";

            internal static string Receipts(string accountId)
                => $"{BASE_URL}/api/receipts/v1/account/{accountId}/receipts";
            
            internal static string Timeline
                => $"{BASE_URL}/api/calendar/v1/timeline";
        }

        internal struct Friends
        {
            private const string BASE_URL = "https://friends-public-service-prod.ol.epicgames.com/friends";

            internal static string Summary(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/summary";

            internal static string AllFriends(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/friends";

            internal static string Incoming(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/incoming";

            internal static string Outgoing(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/outgoing";

            internal static string Blocklist(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/blocklist";

            internal static string Settings(string accountId)
                => $"{BASE_URL}/api/v1/{accountId}/settings";

            internal static string Friend(string accountId, string friendId)
                => $"{BASE_URL}/api/v1/{accountId}/friends/{friendId}";

            internal static string Alias(string accountId, string friendId)
                => $"{BASE_URL}/api/v1/{accountId}/friends/{friendId}/alias";

            internal static string Note(string accountId, string friendId)
                => $"{BASE_URL}/api/v1/{accountId}/friends/{friendId}/note";

            internal static string Block(string accountId, string friendId)
                => $"{BASE_URL}/api/v1/{accountId}/blocklist/{friendId}";
        }

        internal struct Party
        {
            private const string BASE_URL = "https://party-service-prod.ol.epicgames.com/party/api/v1/Fortnite";

            internal static string Parties
                => $"{BASE_URL}/parties";

            internal static string QueryParty(string partyId)
                => $"{Parties}/{partyId}";

            internal static string PartyPings(string accountId, string pingerId)
                => $"{BASE_URL}/user/{accountId}/pings/{pingerId}/parties";

            internal static string Join(string partyId, string accountId)
                => $"{QueryParty(partyId)}/members/{accountId}/join";

            internal static string Member(string partyId, string accountId)
                => $"{QueryParty(partyId)}/members/{accountId}";
            
            internal static string MemberMeta(string partyId, string accountId)
                => $"{Member(partyId, accountId)}/meta";
            
            internal static string ConfirmMember(string partyId, string accountId)
                => $"{Member(partyId, accountId)}/confirm";
        }
    }
}