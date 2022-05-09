using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FortniteDotNet.Enums.FortniteService
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ItemCategory
    {
        Character,
        Dance,
        Glider,
        Pickaxe,
        Hat,
        Backpack,
        LoadingScreen,
        BattleBus,
        VehicleDecoration,
        CallingCard,
        MapMarker,
        ConsumableEmote,
        VictoryPose,
        SkyDiveContrail,
        MusicPack,
        ItemWrap,
        PetSkin,
        Charm
    }
}