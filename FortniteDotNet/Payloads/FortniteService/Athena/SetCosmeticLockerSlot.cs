using System.Collections.Generic;
using FortniteDotNet.Enums.FortniteService;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class SetCosmeticLockerSlot
    {
        [JsonProperty("lockerItem")]
        public string LockerItem { get; set; }

        [JsonProperty("category")]
        public ItemCategory Category { get; set; }

        [JsonProperty("itemToSlot")]
        public string ItemToSlot { get; set; }

        [JsonProperty("slotIndex")]
        public int SlotIndex { get; set; }

        [JsonProperty("variantUpdates")]
        public List<VariantUpdate> VariantUpdates { get; set; }

        [JsonProperty("optLockerUseCountOverride")]
        public int OptLockerUseCountOverride { get; set; } // 0 or -1
    }

    public class VariantUpdate
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("owned")]
        public List<string> Owned { get; set; }
    }
}