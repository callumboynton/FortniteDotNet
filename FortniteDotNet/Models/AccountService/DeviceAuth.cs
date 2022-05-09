using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.AccountService
{
    public class DeviceAuth
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("created")]
        public Geolocation Created { get; set; }

        [JsonProperty("updated")]
        public Geolocation Updated { get; set; }
    }

    public class Geolocation
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }
        
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}