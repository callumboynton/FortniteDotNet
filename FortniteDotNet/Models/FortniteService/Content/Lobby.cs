using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class Lobby : BasePagesEntry
    {
        [JsonProperty("stage")]
        public string Stage { get; set; }
        
        [JsonProperty("backgroundimage")]
        public string BackgroundImage { get; set; }
    }
}