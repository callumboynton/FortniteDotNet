using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Storefront
{
    public class CatalogEntryRequirements
    {
        [JsonProperty("requirementType")]
        public string RequirementType { get; set; }

        [JsonProperty("requiredId")]
        public string RequiredId { get; set; }

        [JsonProperty("minQuantity")]
        public int MinQuantity { get; set; }
    }
}