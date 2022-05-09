using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class SetRandomCosmeticLoadoutFlag
    {
        [JsonProperty("random")]
        public bool random { get; set; }
    }
}