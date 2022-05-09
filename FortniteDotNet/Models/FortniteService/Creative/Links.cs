using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Creative
{
    public class LinkQuery
    {
        [JsonProperty("results")]
        public List<LinkQueryResult> Results { get; set; }

        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }
    }

    public class LinkQueryResult
    {
        [JsonProperty("sortDate")]
        public DateTime SortDate { get; set; }

        [JsonProperty("lastVisited")]
        public DateTime LastVisited { get; set; }

        [JsonProperty("linkData")]
        public LinkEntry LinkData { get; set; }

        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; }
    }
    
    public class LinkEntry
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("creatorName")]
        public string CreatorName { get; set; }

        [JsonProperty("mnemonic")]
        public string Mnemonic { get; set; }

        [JsonProperty("linkType")]
        public string LinkType { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, object> Metadata { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("published")]
        public DateTime Published { get; set; }

        [JsonProperty("descriptionTags")]
        public List<string> DescriptionTags { get; set; }

        [JsonProperty("moderationStatus")]
        public string ModerationStatus { get; set; }
    }
}