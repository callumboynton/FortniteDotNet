using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile
{
    public class Inventory
    {
        [JsonProperty("stash")]
        public Stash Stash { get; set; }
    }

    public class Stash
    {
        [JsonProperty("globalcash")]
        public int GlobalCash { get; set; }
    }
}