namespace FortniteDotNet.Models.AccountService
{
    public class GrantType
    {
        protected string Type { get; set; }
        
        protected GrantType(string grantType)
        {
            Type = grantType;
        }

        public override string ToString()
            => $"grant_type={Type}";
    }
}