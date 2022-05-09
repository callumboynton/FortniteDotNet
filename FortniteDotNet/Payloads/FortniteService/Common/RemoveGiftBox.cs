using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class RemoveGiftBox
    {
        [JsonProperty("giftBoxItemIds")]
        public List<string> GiftBoxItemIds { get; set; }
    }
}