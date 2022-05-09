using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class PlaylistDataMeta
    {
        [JsonProperty("PlaylistData")] 
        public PlaylistData Data { get; set; }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);

        public PlaylistDataMeta(string playlistName)
        {
            Data = new PlaylistData(playlistName);
        }
    }

    public class PlaylistData
    {
        [JsonProperty("playlistName")] 
        public string PlaylistName { get; set; }

        [JsonProperty("tournamentId")] 
        public string TournamentId => "";

        [JsonProperty("eventWindowId")] 
        public string EventWindowId => "";

        [JsonProperty("regionId")] 
        public string RegionId => "EU";

        public PlaylistData(string playlistName)
        {
            PlaylistName = playlistName;
        }
    }
}