using System;

namespace FortniteDotNet.Models.AccountService.GrantTypes
{
    public class ExternalAuth : GrantType
    {
        internal string AuthType { get; set; }
        internal string AuthToken { get; set; }
        
        public ExternalAuth(string authType, string authToken)
            : base("external_auth")
        {
            if (authType == null)
                throw new ArgumentNullException(nameof(authType));
            if (string.IsNullOrWhiteSpace(authType))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authType));

            AuthType = authType;

            if (authToken == null)
                throw new ArgumentNullException(nameof(authToken));
            if (string.IsNullOrWhiteSpace(authToken))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authToken));

            AuthToken = authToken;
        }

        public override string ToString()
            => $"{base.ToString()}&external_auth_type={AuthType}&external_auth_token={AuthToken}";
    }
}
