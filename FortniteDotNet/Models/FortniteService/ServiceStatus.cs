using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService
{
    public class ServiceStatus
    {
        [JsonProperty("serviceInstanceId")]
        public string ServiceInstanceId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("maintenanceUrl")]
        public string MaintenanceUrl { get; set; }

        [JsonProperty("overrideCatalogIds")]
        public string[] OverrideCatalogIds { get; set; }

        [JsonProperty("allowedActions")]
        public string[] AllowedActions { get; set; }

        [JsonProperty("banned")]
        public bool Banned { get; set; }

        [JsonProperty("launcherInfoDTO")]
        public LauncherInfo LauncherInfoDTO { get; set; }
    }
    
    public class LauncherInfo
    {
        [JsonProperty("appName")]
        public string AppName { get; set; }

        [JsonProperty("catalogItemId")]
        public string CatalogItemId { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}