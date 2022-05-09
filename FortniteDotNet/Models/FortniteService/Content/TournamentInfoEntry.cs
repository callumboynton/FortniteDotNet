using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class TournamentInfoEntry : BasePagesEntry
    {
        [JsonProperty("tournament_info")]
        public TournamentInfo TournamentInfo { get; set; }
    }

    public class TournamentInfo
    {
        [JsonProperty("_type")] 
        public string Type { get; set; }
            
        [JsonProperty("tournaments")]
        public List<Tournament> Tournaments { get; set; }
    }
    
    public class Tournament
    {
        [JsonProperty("title_color")] 
        public string TitleColor { get; set; }

        [JsonProperty("loading_screen_image")]
        public string LoadingScreenImage { get; set; }

        [JsonProperty("background_text_color")]
        public string BackgroundTextColor { get; set; }

        [JsonProperty("background_right_color")]
        public string BackgroundRightColor { get; set; }

        [JsonProperty("poster_back_image")]
        public string PosterBackImage { get; set; }

        [JsonProperty("_type")] 
        public string Type { get; set; }

        [JsonProperty("pin_earned_text")] 
        public string PinEarnedText { get; set; }

        [JsonProperty("tournament_display_id")]
        public string TournamentDisplayId { get; set; }

        [JsonProperty("schedule_info")]
        public string ScheduleInfo { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("flavor_description")]
        public string FlavorDescription { get; set; }

        [JsonProperty("poster_front_image")]
        public string PosterFrontImage { get; set; }

        [JsonProperty("short_format_title")]
        public string ShortFormatTitle { get; set; }

        [JsonProperty("title_line_2")]
        public string TitleLine2 { get; set; }

        [JsonProperty("title_line_1")]
        public string TitleLine1 { get; set; }

        [JsonProperty("shadow_color")]
        public string ShadowColor { get; set; }

        [JsonProperty("details_description")]
        public string DetailsDescription { get; set; }

        [JsonProperty("background_left_color")]
        public string BackgroundLeftColor { get; set; }

        [JsonProperty("long_format_title")]
        public string LongFormatTitle { get; set; }

        [JsonProperty("poster_fade_color")]
        public string PosterFadeColor { get; set; }

        [JsonProperty("secondary_color")]
        public string SecondaryColor { get; set; }

        [JsonProperty("playlist_tile_image")]
        public string PlaylistTileImage { get; set; }

        [JsonProperty("base_color")] 
        public string BaseColor { get; set; }
    }
}