using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class MarkNewQuestNotificationSent
    {
        [JsonProperty("itemIds")]
        public List<string> ItemIds { get; set; }
    }
}