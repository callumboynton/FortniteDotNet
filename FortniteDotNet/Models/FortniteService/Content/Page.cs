using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Content
{
    public class Pages : BasePagesEntry
    {
        [JsonProperty("subgameinfo")]
        public SubGameInfoEntry SubGameInfo { get; set; }

        [JsonProperty("subgameselectdata")]
        public SubGameSelectEntry SubGameSelectData { get; set; }

        [JsonProperty("battleroyalenews")]
        public BattleRoyaleNewsEntry BattleRoyaleNews { get; set; }

        [JsonProperty("emergencynotice")]
        public EmergencyNotice EmergencyNotice { get; set; }
        
        [JsonProperty("tournamentinformation")]
        public TournamentInfoEntry TournamentInformation { get; set; }
        
        [JsonProperty("playlistinformation")]
        public PlaylistInfoEntry PlaylistInformation { get; set; }
        
        [JsonProperty("lobby")]
        public Lobby Lobby { get; set; }

        [JsonProperty("dynamicbackgrounds")]
        public DynamicBackgrounds DynamicBackgrounds { get; set; }

        [JsonProperty("loginmessage")]
        public LoginMessageEntry LoginMessage { get; set; }
    }
    
    public class BasePagesEntry
    {
        [JsonProperty("_title")]
        public string Title { get; set; }

        [JsonProperty("_activeDate")]
        public DateTime ActiveDate { get; set; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("_locale")]
        public string Locale => "en-US";
    }
}