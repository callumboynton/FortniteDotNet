using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Calendar.States
{
    public class FeaturedIslandsState
    {
        [JsonProperty("islandCodes")]
        public List<string> IslandCodes { get; set; }

        [JsonProperty("playlistCuratedContent")]
        public object PlaylistCuratedContent { get; set; }

        [JsonProperty("playlistCuratedHub")]
        public Dictionary<string, string> PlaylistCuratedHub { get; set; }

        [JsonProperty("islandTemplates")]
        public object[] IslandTemplates { get; set; }
    }
}