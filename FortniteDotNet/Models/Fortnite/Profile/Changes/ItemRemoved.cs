using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Changes
{
    public class ItemRemoved : BaseProfileChange
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
    }
}