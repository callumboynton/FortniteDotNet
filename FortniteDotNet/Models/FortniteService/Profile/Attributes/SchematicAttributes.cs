using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class SchematicAttributes
    {
        [JsonProperty("legacy_alterations")]
        public List<string> LegacyAlterations { get; set; }
        
        [JsonProperty("refund_legacy_item")]
        public bool RefundLegacyItem { get; set; }
        
        [JsonProperty("alterations")]
        public List<string> Alterations { get; set; }
        
        [JsonProperty("refundable")]
        public bool Refundable { get; set; }
        
        [JsonProperty("alteration_base_rarities")]
        public List<string> AlterationBaseRarities { get; set; }
    }
}