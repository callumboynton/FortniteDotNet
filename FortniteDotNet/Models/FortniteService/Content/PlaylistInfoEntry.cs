using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class PlaylistInfoEntry : BasePagesEntry
    {
        [JsonProperty("frontend_matchmaking_header_style")]
        public string FrontendMatchmakingHeaderStyle { get; set; }
        
        [JsonProperty("frontend_matchmaking_header_text ")]
        public string FrontendMatchmakingHeaderText { get; set; }
        
        [JsonProperty("playlist_info")]
        public PlaylistInfo PlaylistInfo { get; set; }
    }

    public class PlaylistInfo
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
        
        [JsonProperty("playlists")]
        public List<Playlist> Playlists { get; set; }
    }

    public class Playlist
    {
        [JsonProperty("playlist_name")]
        public string PlaylistName { get; set; }
        
        [JsonProperty("display_subname")]
        public string DisplaySubname { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }
        
        [JsonProperty("special_border")]
        public string SpecialBorder { get; set; }
        
        [JsonProperty("violator")]
        public string Violator { get; set; }

        [JsonProperty("hidden")] 
        public bool Hidden { get; set; }
        
        [JsonProperty("_type")] 
        public string Type { get; set; }
    }
}