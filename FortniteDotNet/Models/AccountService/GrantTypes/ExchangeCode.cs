using System;

namespace FortniteDotNet.Models.AccountService.GrantTypes
{
    public class ExchangeCode : GrantType
    {
        internal string Code { get; set; }
        
        public ExchangeCode(string code)
            : base("exchange_code")
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("The parameter must have some value to it.", nameof(code));

            Code = code;
        }

        public override string ToString()
            => $"{base.ToString()}&exchange_code={Code}";
    }
}