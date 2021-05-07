using System;
using System.Threading.Tasks;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Enums.Accounts;

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

                var client = new XMPPClient(authSession);
                await client.Initialize();
                
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}