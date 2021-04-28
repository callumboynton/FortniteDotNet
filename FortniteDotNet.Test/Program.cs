using System.Threading.Tasks;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Enums.Events;

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
                        { "device_id", "03fabe92f86844a6971ea7ff55cc01a6" },
                        { "account_id", "8d89cb32d31147f28e0872116945c376" },
                        { "secret", "KICCZ3GETFF7VGMDJPQRVVBJKVHD4LOM" }
                    });

                await authSession.GetEventDataAsync(Region.EU, Platform.Windows);

            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}