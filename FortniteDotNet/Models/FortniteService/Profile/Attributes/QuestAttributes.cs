using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class QuestAttributes : BaseAttributes
    {
        [JsonProperty("creation_time")]
        public object CreationTime { get; set; }
        
        [JsonProperty("sent_new_notification")]
        public bool SentNewNotification { get; set; }
        
        [JsonProperty("challenge_bundle_id")]
        public string ChallengeBundleId { get; set; }
        
        [JsonProperty("xp_reward_scalar")]
        public int XpRewardScalar { get; set; }
        
        [JsonProperty("challenge_linked_quest_given")]
        public string ChallengeLinkedQuestGiven { get; set; }
        
        [JsonProperty("quest_pool")]
        public string QuestPool { get; set; }
        
        [JsonProperty("quest_state")]
        public string QuestState { get; set; }

        [JsonProperty("last_state_change_time")]
        public DateTime LastStateChangeTime { get; set; }
        
        [JsonProperty("challenge_linked_quest_parent")]
        public string ChallengeLinkedQuestParent { get; set; }
        
        [JsonProperty("quest_rarity")]
        public string QuestRarity { get; set; }
    }
}