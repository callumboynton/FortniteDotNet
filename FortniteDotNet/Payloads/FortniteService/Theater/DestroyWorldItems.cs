using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Theater
{
    public class DestroyWorldItems
    {
        [JsonProperty("itemIds")]
        public List<string> ItemIds { get; set; }
    }
}