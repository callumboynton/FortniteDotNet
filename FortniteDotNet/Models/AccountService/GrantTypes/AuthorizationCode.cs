using System;

namespace FortniteDotNet.Models.AccountService.GrantTypes
{
    public class AuthorizationCode : GrantType
    {
        internal string Code { get; set; }
        
        public AuthorizationCode(string code)
            : base("authorization_code")
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("The parameter must have some value to it.", nameof(code));

            Code = code;
        }

        public override string ToString()
            => $"{base.ToString()}&code={Code}";
    }
}