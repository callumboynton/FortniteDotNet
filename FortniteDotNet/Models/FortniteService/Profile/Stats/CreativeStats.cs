using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Stats
{
    public class CreativeStats : BaseStats
    {
        [JsonProperty("last_used_battlelab_file")]
        public string LastUsedBattleLabFile { get; set; }

        [JsonProperty("max_island_plots")]
        public int MaxIslandPlots { get; set; }

        [JsonProperty("publish_allowed")]
        public bool PublishAllowed { get; set; }

        [JsonProperty("support_code")]
        public string SupportCode { get; set; }

        [JsonProperty("last_used_plot")]
        public string LastUsedPlot { get; set; }

        [JsonProperty("permissions")]
        public object[] Permissions { get; set; }

        [JsonProperty("creator_name")]
        public string CreatorName { get; set; }

        [JsonProperty("publish_banned")]
        public bool PublishBanned { get; set; }
    }
}