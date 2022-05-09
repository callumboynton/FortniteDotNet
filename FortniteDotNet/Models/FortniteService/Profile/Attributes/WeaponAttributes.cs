using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class WeaponAttributes : WorldItemAttributes
    {
        [JsonProperty("clipSizeScale")]
        public int ClipSizeScale { get; set; }

        [JsonProperty("baseClipSize")]
        public int BaseClipSize { get; set; }
    }
}