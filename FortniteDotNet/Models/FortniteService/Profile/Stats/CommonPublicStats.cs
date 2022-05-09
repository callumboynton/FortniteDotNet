using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Stats
{
    public class CommonPublicStats
    {
        [JsonProperty("banner_color")]
        public string BannerColor { get; set; }
        
        [JsonProperty("banner_icon")]
        public string BannerIcon { get; set; }
        
        [JsonProperty("homebase_name")]
        public string HomebaseName { get; set; }
    }
}