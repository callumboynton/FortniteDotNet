using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class EquipCampaignCustomization
    {
        [JsonProperty("slotName")]
        public string SlotName { get; set; }
        
        [JsonProperty("itemToSlot")]
        public string ItemToSlot { get; set; }
    }
}