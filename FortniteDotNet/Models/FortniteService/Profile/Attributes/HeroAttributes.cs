using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class HeroAttributes : BaseAttributes
    {
        [JsonProperty("outfitvariants")]
        public List<object> OutfitVariants { get; set; }

        [JsonProperty("backblingvariants")]
        public List<object> BackblingVariants { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("squad_slot_idx")]
        public int SquadSlotId { get; set; }

        [JsonProperty("portrait")]
        public string Portrait { get; set; }

        [JsonProperty("hero_name")]
        public string HeroName { get; set; }

        [JsonProperty("squad_id")]
        public string SquadId { get; set; }

        [JsonProperty("mode_loadouts")]
        public List<object> ModeLoadouts { get; set; }

        [JsonProperty("slotted_building_id")]
        public string SlottedBuildingId { get; set; }

        [JsonProperty("refundable")]
        public bool Refundable { get; set; }

        [JsonProperty("building_slot_used")]
        public int BuildingSlotUsed { get; set; }
    }
}