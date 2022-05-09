using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FortniteDotNet.Payloads.FortniteService.Theater
{
    public class CraftWorldItem
    {
        [JsonProperty("targetSchematicItemId")]
        public string TargetSchematicItemId { get; set; }

        [JsonProperty("numTimesToCraft")]
        public int NumTimesToCraft { get; set; }

        [JsonProperty("targetSchematicTier")]
        public SchematicTier TargetSchematicTier { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(LowerCaseNamingStrategy))]
    public enum SchematicTier
    {
        I,
        II,
        III,
        IV,
        V,
        VI,
        NO_TIER
    }

    public class LowerCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLower();
        }
    }
}