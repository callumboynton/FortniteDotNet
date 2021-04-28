using System;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Common
{
    internal class EpicException : Exception
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("messageVars")]
        public object[] MessageVars { get; set; }

        [JsonProperty("numericErrorCode")]
        public int NumericErrorCode { get; set; }

        [JsonProperty("originatingService")]
        public string OriginatingService { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        public EpicException(string errorMessage, string errorCode)
            : base(!string.IsNullOrEmpty(errorMessage) ? errorMessage : errorCode)
        {
        }
    }
}