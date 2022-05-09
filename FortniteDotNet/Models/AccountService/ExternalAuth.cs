using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.AccountService
{
    public class ExternalAuth
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("externalAuthId")] 
        public string ExternalAuthId { get; set; }

        [JsonProperty("externalAuthIdType")] 
        public string ExternalAuthIdType { get; set; }

        [JsonProperty("externalDisplayName")] 
        public string ExternalDisplayName { get; set; }

        [JsonProperty("authIds")]
        public List<AuthId> AuthIds { get; set; }

        [JsonProperty("dateAdded")] 
        public DateTime DateAdded { get; set; }
    }

    public class AuthId
    {
        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }
    }
}