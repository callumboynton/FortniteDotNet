using System;

namespace FortniteDotNet.Models.AccountService.GrantTypes
{
    public class RefreshToken : GrantType
    {
        internal string Token { get; set; }
        
        public RefreshToken(string token)
            : base("refresh_token")
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("The parameter must have some value to it.", nameof(token));

            Token = token;
        }

        public override string ToString()
            => $"{base.ToString()}&refresh_token={Token}";
    }
}