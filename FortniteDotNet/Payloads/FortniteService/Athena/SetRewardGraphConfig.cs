using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class SetRewardGraphConfig
    {
        [JsonProperty("state")]
        public List<string> State { get; set; }
        
        [JsonProperty("rewardGraphId")]
        public string RewardGraphId { get; set; }
    }
}