using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class FortRerollDailyQuest
    {
        [JsonProperty("questId")]
        public string QuestId { get; set; }
    }
}