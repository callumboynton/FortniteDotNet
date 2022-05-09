using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class RecycleItemBatch
    {
        [JsonProperty("targetItemIds")]
        public List<string> TargetItemIds { get; set; }
    }
}