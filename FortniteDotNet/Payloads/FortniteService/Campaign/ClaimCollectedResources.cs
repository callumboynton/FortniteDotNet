using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ClaimCollectedResources
    {
        [JsonProperty("collectorsToClaim")]
        public List<string> CollectorsToClaim { get; set; }
    }
}