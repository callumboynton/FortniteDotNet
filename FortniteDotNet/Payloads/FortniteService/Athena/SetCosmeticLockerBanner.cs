using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class SetCosmeticLockerBanner
    {
        [JsonProperty("bannerColorTemplateName")]
        public string ColorTemplateId { get; set; }
        
        [JsonProperty("bannerIconTemplateName")]
        public string IconTemplateId { get; set; }
        
        [JsonProperty("lockerItem")]
        public string LockerItem { get; set; }
    }
}