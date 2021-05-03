using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class CosmeticVariantTokenAttributes : BaseAttributes
    {
        [JsonProperty("cosmetic_item")]
        public string CosmeticItem { get; set; }
        
        [JsonProperty("variant_name")]
        public string VariantName { get; set; }
        
        [JsonProperty("create_giftbox")]
        public bool CreateGiftbox { get; set; }
        
        [JsonProperty("mark_item_unseen")]
        public bool MarkItemUnseen { get; set; }
        
        [JsonProperty("custom_giftbox")]
        public string CustomGiftbox { get; set; }
        
        [JsonProperty("auto_equip_variant")]
        public bool AutoEquipVariant { get; set; }
        
        [JsonProperty("variant_channel")]
        public string VariantChannel { get; set; }
    }
}