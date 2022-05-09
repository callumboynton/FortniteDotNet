using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class SetReceiveGiftsEnabled
    {
        [JsonProperty("bReceiveGifts")]
        public bool ReceiveGifts { get; set; }
    }
}