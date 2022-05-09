using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FriendsService
{
    public class Connection
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("sortFactors")]
        public SortFactors SortFactors { get; set; }
    }
    
    public class SortFactors
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("k")]
        public DateTime K { get; set; }

        [JsonProperty("l")]
        public DateTime L { get; set; }
    }
}