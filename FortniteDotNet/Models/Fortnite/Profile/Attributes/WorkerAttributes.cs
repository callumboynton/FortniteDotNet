using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class WorkerAttributes : BaseAttributes
    {
        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("squad_slot_idx")]
        public int SquadSlotId { get; set; }

        [JsonProperty("portrait")]
        public string Portrait { get; set; }

        [JsonProperty("personality")]
        public string Personality { get; set; }

        [JsonProperty("squad_id")]
        public string SquadId { get; set; }

        [JsonProperty("slotted_building_id")]
        public string SlottedBuildingId { get; set; }

        [JsonProperty("building_slot_used")]
        public int BuildingSlotUsed { get; set; }

        [JsonProperty("set_bonus")]
        public string SetBonus { get; set; }
    }
}