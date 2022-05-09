using System;
using Newtonsoft.Json;

namespace FortniteDotNet
{
    public class EpicException : Exception
    {
        [JsonConstructor]
        internal EpicException(string errorCode, string errorMessage, string intent, object[] messageVars, int numericErrorCode, string originatingService)
            : base(!string.IsNullOrEmpty(errorMessage) ? errorMessage : errorCode)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Intent = intent;
            MessageVars = messageVars;
            NumericErrorCode = numericErrorCode;
            OriginatingService = originatingService;
        }

        [JsonProperty("errorCode")] 
        public string ErrorCode { get; internal set; }

        [JsonProperty("errorMessage")] 
        public string ErrorMessage { get; internal set; }

        [JsonProperty("messageVars")] 
        public object[] MessageVars { get; internal set; }

        [JsonProperty("numericErrorCode")] 
        public int NumericErrorCode { get; internal set; }

        [JsonProperty("originatingService")] 
        public string OriginatingService { get; internal set; }

        [JsonProperty("intent")] 
        public string Intent { get; internal set; }
        
        [JsonIgnore]
        public string RawException { get; internal set; }
    }
}