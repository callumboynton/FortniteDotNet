using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class SetHomebaseBanner
    {
        [JsonProperty("homebaseBannerColorId")]
        public string BannerColorId { get; set; }
        
        [JsonProperty("homebaseBannerIconId")]
        public string BannerIconId { get; set; }
    }
}