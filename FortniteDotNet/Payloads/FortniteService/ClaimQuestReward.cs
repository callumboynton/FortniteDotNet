using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService
{
    public class ClaimQuestReward
    {
        [JsonProperty("questId")]
        public string QuestId { get; set; }
    }
}