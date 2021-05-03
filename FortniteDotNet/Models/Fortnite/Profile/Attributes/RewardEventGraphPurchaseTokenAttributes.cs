using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class RewardEventGraphPurchaseTokenAttributes : BaseAttributes
    {
        [JsonProperty("mini_challenges_templateId")]
        public string MiniChallengesTemplateId { get; set; }
        
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }
    }
}