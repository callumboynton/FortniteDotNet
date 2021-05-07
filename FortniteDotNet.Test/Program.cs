using System;
using System.Text;
using System.Threading.Tasks;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Models.XMPP;

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
                client.OnMessage += async (_, args) =>
                {
                    Console.WriteLine(args.MessageType);
                };
                await client.Initialize();
                
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}