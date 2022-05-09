using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class CollectableCharacterAttributes
    {
        [JsonProperty("collected")]
        public List<CollectedCharacter> Collected { get; set; }
    }

    public class CollectedCharacter
    {
        [JsonProperty("variantTag")]
        public string VariantTag { get; set; }

        [JsonProperty("contextTags")]
        public List<string> ContextTags { get; set; }

        [JsonProperty("properties")]
        public CharacterProperties Properties { get; set; }

        [JsonProperty("seenState")]
        public string SeenState { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
    
    public class CharacterProperties
    {
        [JsonProperty("questsGiven")]
        public int QuestsGiven { get; set; }

        [JsonProperty("questsCompleted")]
        public int QuestsCompleted { get; set; }

        [JsonProperty("encounterTypeFlags")]
        public int EncounterTypeFlags { get; set; }
    }
}