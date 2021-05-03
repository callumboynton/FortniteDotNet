using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Changes
{
    public class ItemAdded : BaseProfileChange
    {
        [JsonProperty("item")] 
        public ProfileItem Item { get; set; }
        
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
    }
}