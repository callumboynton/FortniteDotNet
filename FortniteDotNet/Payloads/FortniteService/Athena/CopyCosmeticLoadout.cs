using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class CopyCosmeticLoadout
    {
        [JsonProperty("OptNewNameForTarget")]
        public string optNewNameForTarget { get; set; }
        
        [JsonProperty("sourceIndex")]
        public int SourceIndex { get; set; }
        
        [JsonProperty("targetIndex")]
        public int TargetIndex { get; set; }
    }
}