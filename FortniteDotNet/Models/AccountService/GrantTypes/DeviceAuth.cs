using System;

namespace FortniteDotNet.Models.AccountService.GrantTypes
{
    public class DeviceAuth : GrantType
    {
        internal string AccountId { get; set; }
        internal string DeviceId { get; set; }
        internal string Secret { get; set; }
        
        public DeviceAuth(string accountId, string deviceId, string secret)
            : base("device_auth")
        {
            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(accountId));

            AccountId = accountId;

            if (deviceId == null)
                throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("The parameter must have some value to it.", nameof(deviceId));

            DeviceId = deviceId;

            if (secret == null)
                throw new ArgumentNullException(nameof(secret));
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("The parameter must have some value to it.", nameof(secret));

            Secret = secret;
        }

        public override string ToString()
            => $"{base.ToString()}&account_id={AccountId}&device_id={DeviceId}&secret={Secret}";
    }
}