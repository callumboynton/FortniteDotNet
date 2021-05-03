using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class CurrencyAttributes
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}