using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class ChallengeBundleAttributes : BaseAttributes
    {
        [JsonProperty("has_unlock_by_completion")]
        public bool HasUnlockByCompletion { get; set; }
        
        [JsonProperty("num_quests_completed")]
        public int NumQuestsCompleted { get; set; }
        
        [JsonProperty("grantedquestinstanceids")]
        public List<string> GrantedQuestInstanceIds { get; set; }
        
        [JsonProperty("max_allowed_bundle_level")]
        public int MaxAllowedBundleLevel { get; set; }

        [JsonProperty("num_granted_bundle_quests")]
        public int NumGrantedBundleQuests { get; set; }
        
        [JsonProperty("challenge_bundle_schedule_id")]
        public string ChallengeBundleScheduleId { get; set; }
        
        [JsonProperty("num_progress_quests_completed")]
        public int NumProgressQuestsCompleted { get; set; }
    }
}