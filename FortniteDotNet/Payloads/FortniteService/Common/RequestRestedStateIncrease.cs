using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class RequestRestedStateIncrease
    {
        [JsonProperty("timeToCompensateFor")]
        public int TimeToCompensateFor { get; set; }

        [JsonProperty("restedXpGenAccumulated")]
        public int RestedXpGenAccumulated { get; set; }
    }
}