using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class CurrencyAttributes
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}