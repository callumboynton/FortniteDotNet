﻿using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Profile.Changes
{
    public class FullProfileUpdate : BaseProfileChange
    {
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }
}