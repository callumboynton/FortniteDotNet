using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Storefront
{
    public class CatalogEntryItemGrant
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}