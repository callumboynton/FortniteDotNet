using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Stats
{
    public class BaseStats
    {
        [JsonProperty("inventory_limit_bonus")]
        public int InventoryLimitBonus { get; set; }
    }
}