using FortniteDotNet.Enums.FortniteService;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public class SetMtxPlatform
    {
        [JsonProperty("newPlatform")]
        public MtxPlatform NewPlatform { get; set; }
    }
}