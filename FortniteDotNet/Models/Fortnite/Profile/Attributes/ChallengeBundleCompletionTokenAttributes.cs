using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile.Attributes
{
    public class ChallengeBundleCompletionTokenAttributes : BaseAttributes
    {
        [JsonProperty("completion_bits")]
        public List<int> CompletionBits { get; set; }
        
        [JsonProperty("bundle_level")]
        public int BundleLevel { get; set; }
        
        [JsonProperty("progress_tracker")]
        public List<ChallengeProgress> ProgressTracker { get; set; }
        
        [JsonProperty("bundle_max_allowed_level")]
        public int BundleMaxAllowedLevel { get; set; }
        
        [JsonProperty("completion_times")]
        public List<int> CompletionTimes { get; set; }
        
        [JsonProperty("bundle_template")]
        public string BundleTemplate { get; set; }
        
        [JsonProperty("bundle_id")]
        public string BundleId { get; set; }
    }

    public class ChallengeProgress
    {
        [JsonProperty("questTemplate")]
        public string QuestTemplate { get; set; }
        
        [JsonProperty("partialData")]
        public List<ProgressData> PartialData { get; set; }
    }

    public class ProgressData
    {
        [JsonProperty("objectiveName")]
        public string ObjectiveName { get; set; }
        
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}