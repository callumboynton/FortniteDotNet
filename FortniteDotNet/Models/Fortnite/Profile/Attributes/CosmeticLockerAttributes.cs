using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class CosmeticLockerAttributes
    {
        [JsonProperty("locker_slots_data")]
        public LockerSlotsData LockerSlotsData { get; set; }
        
        [JsonProperty("use_count")]
        public int UseCount { get; set; }
        
        [JsonProperty("banner_icon_template")]
        public string BannerIconTemplate { get; set; }
        
        [JsonProperty("banner_color_template")]
        public string BannerColorTemplate { get; set; }
        
        [JsonProperty("locker_name")]
        public string LockerName { get; set; }
        
        [JsonProperty("item_seen")]
        public bool ItemSeen { get; set; }
        
        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
    }

    public class LockerSlotsData
    {
        [JsonProperty("slots")]
        public Dictionary<string, Slot> Slots { get; set; }
    }

    public class Slot
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }
        
        [JsonProperty("activeVariants")]
        public List<object> ActiveVariants { get; set; }
    }
}