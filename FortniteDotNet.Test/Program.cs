using System;
using System.Threading;
using System.Threading.Tasks;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Test
{
    internal class Program
    {
        private static OAuthSession _authSession;
        
        private static void Main()
        {
            var api = new FortniteApi();
            Task.Run(async () =>
            {
                _authSession = await api.AccountService.GenerateOAuthSession(GrantType.DeviceAuth, AuthClient.iOS, new()
                {
                    { "device_id", "" },
                    { "account_id", "" },
                    { "secret", "" }
                });
                
            }).ConfigureAwait(false).GetAwaiter().GetResult();
                
            var thread = new Thread(Xmpp);
            thread.Start();
            
            Thread.Sleep(2000);
            
            _authSession.SendOrAcceptFriendRequest("d6681ac9e07344e9aeee7562b762d40d");
        }

        private static void Xmpp()
        {
            var client = new XMPPClient(_authSession);
            client.Initialize().GetAwaiter().GetResult();
        }
    }
}