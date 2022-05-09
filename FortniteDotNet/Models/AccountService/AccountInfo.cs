using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.AccountService
{
    public class AccountInfo
    {
        #region Public Info

        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("displayName")] 
        public string DisplayName { get; set; }

        [JsonProperty("links")] 
        public Dictionary<string, object> Links { get; set; }

        [JsonProperty("externalAuths")] 
        public Dictionary<string, ExternalAuth> ExternalAuths { get; set; }

        #endregion

        #region Private Info

        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("email")] 
        public string Email { get; set; }

        [JsonProperty("failedLoginAttempts")] 
        public int FailedLoginAttempts { get; set; }

        [JsonProperty("lastLogin")] 
        public DateTime LastLogin { get; set; }

        [JsonProperty("numberOfDisplayNameChanges")]
        public int NumberOfDisplayNameChanges { get; set; }

        [JsonProperty("ageGroup")] 
        public string AgeGroup { get; set; }

        [JsonProperty("headless")] 
        public bool Headless { get; set; }

        [JsonProperty("country")] 
        public string Country { get; set; }

        [JsonProperty("lastName")] 
        public string LastName { get; set; }

        [JsonProperty("phoneNumber")] 
        public string PhoneNumber { get; set; }

        [JsonProperty("preferredLanguage")] 
        public string PreferredLanguage { get; set; }

        [JsonProperty("lastDisplayNameChange")]
        public DateTime LastDisplayNameChange { get; set; }

        [JsonProperty("canUpdateDisplayName")] 
        public bool CanUpdateDisplayName { get; set; }

        [JsonProperty("tfaEnabled")] 
        public bool TfaEnabled { get; set; }

        [JsonProperty("emailVerified")] 
        public bool EmailVerified { get; set; }

        [JsonProperty("minorVerified")] 
        public bool MinorVerified { get; set; }

        [JsonProperty("minorExpected")] 
        public bool MinorExpected { get; set; }

        [JsonProperty("minorStatus")] 
        public string MinorStatus { get; set; }

        #endregion
    }
}