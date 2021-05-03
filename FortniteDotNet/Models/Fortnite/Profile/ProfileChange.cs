using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite.Profile
{
    public class ProfileChange
    {
        [JsonProperty("changeType")]
        public string ChangeType { get; set; }
        
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        [JsonProperty("_id")]
        public string _Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("rvn")]
        public int Revision { get; set; }

        [JsonProperty("wipeNumber")] 
        public int WipeNumber { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("profileId")]
        public string ProfileId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("items")]
        public Dictionary<string, ProfileItem> Items { get; set; }

        [JsonProperty("stats")]
        public ProfileStats Stats { get; set; }
        
        [JsonProperty("commandRevision")]
        public int CommandRevision { get; set; }
    }

    public class ProfileStats
    {
        [JsonProperty("attributes")]
        public object Attributes { get; set; }
    }
}