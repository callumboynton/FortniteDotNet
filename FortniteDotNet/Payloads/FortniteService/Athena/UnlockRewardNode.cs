using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class UnlockRewardNode
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("rewardGraphId")]
        public string RewardGraphId { get; set; }

        [JsonProperty("rewardCfg")]
        public string RewardCfg { get; set; }
    }
}