using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.Fortnite
{
    public class CloudstorageFile
    {
        [JsonProperty("uniqueFilename")]
        public string UniqueFilename { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("hash256")]
        public string Hash256 { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("uploaded")]
        public DateTime Uploaded { get; set; }

        [JsonProperty("storageType")]
        public string StorageType { get; set; }
        
        [JsonProperty("storageIds")]
        public Dictionary<string, string> StorageIds { get; set; }

        [JsonProperty("doNotCache")]
        public bool DoNotCache { get; set; }
    }
}