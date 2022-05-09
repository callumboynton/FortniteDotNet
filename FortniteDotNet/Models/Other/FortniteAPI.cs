using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Other
{
    public class FortniteAPIResponse<T>
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
    }

    public class Cosmetic
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("added")]
        public DateTime Added { get; set; }
    }

    public class Variant
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("options")]
        public List<VariantOption> Options { get; set; }
    }

    public class VariantOption
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Banner
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}