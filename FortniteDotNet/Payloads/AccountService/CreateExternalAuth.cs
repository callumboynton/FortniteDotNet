using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.AccountService
{
    public class CreateExternalAuth
    {
        public CreateExternalAuth(string authType, string externalAuthToken)
        {
            if (authType == null)
                throw new ArgumentNullException(nameof(authType));
            if (string.IsNullOrWhiteSpace(authType))
                throw new ArgumentException("The parameter must have some value to it.", nameof(authType));

            AuthType = authType;
            
            if (externalAuthToken == null)
                throw new ArgumentNullException(nameof(externalAuthToken));
            if (string.IsNullOrWhiteSpace(externalAuthToken))
                throw new ArgumentException("The parameter must have some value to it.", nameof(externalAuthToken));

            ExternalAuthToken = externalAuthToken;
        }
        
        [JsonProperty("authType")]
        internal string AuthType { get; set; }
        
        [JsonProperty("externalAuthToken")]
        internal string ExternalAuthToken { get; set; }
    }
}