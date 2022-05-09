using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class ChallengeBundleScheduleAttributes : BaseAttributes
    {
        [JsonProperty("unlock_epoch")] 
        public DateTime UnlockEpoch { get; set; }
        
        [JsonProperty("granted_bundles")]
        public List<string> GrantedBundles { get; set; }
    }
}