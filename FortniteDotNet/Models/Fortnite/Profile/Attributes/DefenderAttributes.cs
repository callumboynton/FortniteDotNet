using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class DefenderAttributes : BaseAttributes
    {
        [JsonProperty("squad_id")] 
        public string SquadId { get; set; }
        
        [JsonProperty("squad_slot_idx")] 
        public string SquadSlotId { get; set; }
        
        [JsonProperty("alterations")]
        public List<string> Alterations { get; set; }
    }
}