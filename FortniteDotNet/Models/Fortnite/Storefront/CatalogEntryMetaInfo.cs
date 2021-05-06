using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Storefront
{
    public class CatalogEntryMetaInfo
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}