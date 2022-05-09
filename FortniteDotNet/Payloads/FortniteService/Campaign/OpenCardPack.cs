using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class OpenCardPack
    {
        [JsonProperty("cardPackItemId")]
        public string CardPackItemId { get; set; }
    }
}