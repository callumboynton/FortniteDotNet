using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Attributes
{
    public class CreativePlotAttributes : BaseAttributes
    {
        [JsonProperty("island_code")]
        public string IslandCode;

        [JsonProperty("locale")]
        public string Locale;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("deleted_at")]
        public DateTime DeletedAt;

        [JsonProperty("last_used_date")]
        public string LastUsedDate { get; set; }

        [JsonProperty("vk_project_id")]
        public string VkProjectId { get; set; }

        [JsonProperty("vk_module_id")]
        public string VkModuleId { get; set; }

        [JsonProperty("last_published_date")]
        public string LastPublishedDate { get; set; }

        [JsonProperty("permissions")]
        public object Permissions { get; set; }

        [JsonProperty("youtube_video_id")]
        public string YoutubeVideoId { get; set; }

        [JsonProperty("descriptionTags")]
        public List<string> DescriptionTags { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("islandIndex")]
        public int IslandIndex { get; set; }

        [JsonProperty("last_published_version")]
        public int LastPublishedVersion { get; set; }

        [JsonProperty("introduction")]
        public string Introduction { get; set; }
    }

    public class Permissions
    {
        [JsonProperty("permission")]
        public string Permission { get; set; }
        
        [JsonProperty("whitelist")]
        public List<string> Whitelist { get; set; }
    }
}