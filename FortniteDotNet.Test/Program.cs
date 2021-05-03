using System;
using System.Threading.Tasks;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Models.Fortnite.Profile.Changes;
using FortniteDotNet.Models.Fortnite.Profile.Stats;
using FortniteDotNet.Payloads.Fortnite;

namespace FortniteDotNet.Test
{
    internal class Program
    {
        private static void Main()
        {
            var api = new FortniteApi();

            Task.Run(async () =>
            {
                await using var authSession = await api.AccountService.GenerateOAuthSession(
                    GrantType.DeviceAuth, AuthClient.iOS, new()
                    {
                        { "device_id", "" },
                        { "account_id", "" },
                        { "secret", "" }
                    });

            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}