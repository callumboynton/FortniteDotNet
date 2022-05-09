using FortniteDotNet.Enums.FortniteService;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class GiftCatalogEntry : PurchaseCatalogEntry
    {
        [JsonProperty("receiverAccountIds")]
        public string[] ReceiverAccountIds { get; set; }

        [JsonProperty("giftWrapTemplateId")]
        public GiftBox GiftWrapTemplateId { get; set; }

        [JsonProperty("personalMessage")]
        public string PersonalMessage { get; set; }
    }
}