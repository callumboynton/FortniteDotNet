using System;
using FortniteDotNet.Attributes;

namespace FortniteDotNet.Enums.Accounts
{
    public enum GrantType
    {
        [Value("authorization_code")]
        [RequiredFields("code")]
        AuthorizationCode,
        
        [Value("client_credentials")]
        [RequiredFields]
        ClientCredentials,
        
        [Value("device_auth")]
        [RequiredFields("account_id", "device_id", "secret")]
        DeviceAuth,
        
        [Value("device_code")]
        [RequiredFields("device_code")]
        DeviceCode,
        
        [Value("exchange_code")]
        [RequiredFields("exchange_code")]
        ExchangeCode,
        
        [Value("external_auth")]
        [RequiredFields("external_auth_type", "external_auth_token")]
        ExternalAuth,
        
        [Value("otp")]
        [RequiredFields("otp", "challenge")]
        OTP,
        
        [Value("password")]
        [RequiredFields("username", "password")]
        [Obsolete("This grant type has been deprecated on all public clients.", true)]
        Password,
        
        [Value("refresh_token")]
        [RequiredFields("refresh_token")]
        RefreshToken,
        
        [Value("token_to_token")]
        [RequiredFields("access_token")]
        TokenToToken
    }
}