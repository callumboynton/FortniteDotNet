using System;

namespace FortniteDotNet.Models.AccountService
{
    public class AuthClient
    {
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string Token { get; }
        
        public AuthClient(string clientId, string clientSecret)
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(clientId));

            ClientId = clientId;

            if (clientSecret == null)
                throw new ArgumentNullException(nameof(clientSecret));
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException("The parameter must have some value to it.", nameof(clientSecret));

            ClientSecret = clientSecret;

            Token = $"{clientId}:{clientSecret}".ToBase64();
        }

        public static AuthClient PC
            => new AuthClient("ec684b8c687f479fadea3cb2ad83f5c6", "e1f31c211f28413186262d37a13fc84d");

        public static AuthClient iOS
            => new AuthClient("3446cd72694c4a4485d81b77adbb2141", "9209d4a5e25a457fb9b07489d313b41a");

        public static AuthClient Android
            => new AuthClient("3f69e56c7649492c8cc29f1af08a8a12", "b51ee9cb12234f50a69efa67ef53812e");

        public static AuthClient Switch
            => new AuthClient("5229dcd3ac3845208b496649092f251b", "e3bd2d3e-bf8c-4857-9e7d-f3d947d220c7");

        public static AuthClient Launcher
            => new AuthClient("34a02cf8f4414e29b15921876da36f9a", "daafbccc737745039dffe53d94fc76cf");
    }
}