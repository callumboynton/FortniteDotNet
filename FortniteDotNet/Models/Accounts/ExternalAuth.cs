using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Accounts
{
    public class ExternalAuth
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty("authIds")]
        public List<ExternalAuthId> AuthIds { get; set; }
        
        [JsonProperty("dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("externalAuthId")] 
        public string ExternalAuthId { get; set; }

        [JsonProperty("externalAuthIdType")] 
        public string ExternalAuthIdType { get; set; }

        [JsonProperty("externalAuthSecondaryId")] 
        public string ExternalAuthSecondaryId { get; set; }

        [JsonProperty("externalDisplayName")] 
        public string ExternalDisplayName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ExternalAuthId
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}