using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.FortniteService.Calendar.States
{
    public class CommunityVotesState
    {
        [JsonProperty("electionId")]
        public string ElectionId { get; set; }

        [JsonProperty("candidates")]
        public object[] Candidates { get; set; }

        [JsonProperty("electionEnds")]
        public DateTime ElectionEnds { get; set; }

        [JsonProperty("numWinners")]
        public int NumWinners { get; set; }
    }
}