using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Common
{
    public class CompletePlayerSurvey
    {
        [JsonProperty("surveyId")]
        public string SurveyId { get; set; }
    }
}