using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class MessageOfTheDay
    {
        [JsonProperty("entryType")]
        public string EntryType { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sortingPriority")]
        public int SortingPriority { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class BattleRoyaleMOTD : MessageOfTheDay
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("tileImage")]
        public string TileImage { get; set; }
    }
}