using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class AssignGadgetToLoadout
    {
        [JsonProperty("gadgetId")]
        public string GadgetId { get; set; }

        [JsonProperty("loadoutId")]
        public string LoadoutId { get; set; }

        [JsonProperty("slotIndex")]
        public int SlotIndex { get; set; }
    }
}