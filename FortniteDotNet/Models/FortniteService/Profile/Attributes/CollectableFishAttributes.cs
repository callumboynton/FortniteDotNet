using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class CollectableFishAttributes
    {
        [JsonProperty("collected")]
        public List<CollectedFish> Collected { get; set; }
    }
    
    public class CollectedFish
    {
        [JsonProperty("variantTag")]
        public string VariantTag { get; set; }

        [JsonProperty("contextTags")]
        public List<string> ContextTags { get; set; }

        [JsonProperty("properties")]
        public FishProperties Properties { get; set; }

        [JsonProperty("seenState")]
        public string SeenState { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
    
    public class FishProperties
    {
        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }
    }
}