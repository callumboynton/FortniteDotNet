using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class EmergencyNotice : BasePagesEntry
    {
        [JsonProperty("news")]
        public BattleRoyaleNews News { get; set; }
    }
}