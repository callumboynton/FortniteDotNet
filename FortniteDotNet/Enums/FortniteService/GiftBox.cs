using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FortniteDotNet.Enums.FortniteService
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GiftBox
    {
        [EnumMember(Value = "GiftBox:gb_default")]
        Default,
        
        [EnumMember(Value = "GiftBox:gb_giftwrap1")]
        GiftWrap1,
        
        [EnumMember(Value = "GiftBox:gb_giftwrap2")]
        GiftWrap2,
        
        [EnumMember(Value = "GiftBox:gb_giftwrap3")]
        GiftWrap3,
        
        [EnumMember(Value = "GiftBox:gb_giftwrap4")]
        GiftWrap4
    }
}