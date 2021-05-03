using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Stats
{
    public class TheaterStats : BaseStats
    {
        [JsonProperty("player_loadout")]
        public PlayerLoadout PlayerLoadout { get; set; }

        [JsonProperty("theater_unique_id")]
        public string TheaterUniqueId { get; set; }

        [JsonProperty("past_lifetime_zones_completed")]
        public int PastLifetimeZonesCompleted { get; set; }

        [JsonProperty("last_event_instance_key")]
        public string LastEventInstanceKey { get; set; }

        [JsonProperty("last_zones_completed")]
        public int LastZonesCompleted { get; set; }
    }
    
    public class PlayerLoadout
    {
        [JsonProperty("bPlayerIsNew")]
        public bool PlayerIsNew { get; set; }

        [JsonProperty("pinnedSchematicInstances")]
        public object[] PinnedSchematicInstances { get; set; }

        [JsonProperty("primaryQuickBarRecord")]
        public QuickBarRecord PrimaryQuickBarRecord { get; set; }

        [JsonProperty("secondaryQuickBarRecord")]
        public QuickBarRecord SecondaryQuickBarRecord { get; set; }

        [JsonProperty("zonesCompleted")]
        public int ZonesCompleted { get; set; }
    }

    public class QuickBarRecord
    {
        [JsonProperty("slots")]
        public List<Slot> Slots { get; set; }
    }
    
    public class Slot
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }
}