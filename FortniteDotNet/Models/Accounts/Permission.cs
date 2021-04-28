using Newtonsoft.Json;

namespace FortniteDotNet.Models.Accounts
{
    public class Permission
    {
        [JsonProperty("resource")] 
        public string Resource { get; set; }
        
        [JsonProperty("action")] 
        public int Action { get; set; }
    }
}