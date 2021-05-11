using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Enums.Party;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Test
{
    internal class Program
    {
        private static FortniteApi _api;
        
        private static XMPPClient _xmppClient;
        private static OAuthSession _authSession;
        
        private static void Main()
        {
            _api = new FortniteApi();
            Task.Run(async () =>
            {
                _authSession = await _api.AccountService.GenerateOAuthSession(GrantType.DeviceAuth, AuthClient.iOS, new());
                
                var thread = new Thread(Xmpp);
                thread.Start();
            
                Thread.Sleep(1000);
            
                var party = await _authSession.CreateParty(_xmppClient);
                await _xmppClient.JoinPartyChat();
                await party.UpdatePrivacy(_authSession, new PartyPrivacy(Privacy.Public));

            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void Xmpp()
        {
            _xmppClient = new XMPPClient(_authSession);
            _xmppClient.OnGroupChatReceived += async (_, chat) =>
            {
                if (!chat.Body.StartsWith("!")) 
                    return;

                var args = chat.Body.Remove(0, 1).Split(" ");
                var command = args.FirstOrDefault();
                var content = string.Join(" ", args.Skip(1));

                switch (command)
                {
                    case "emote":
                    {
                        var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
                        await me.SetEmote(_xmppClient, content);
                        break;
                    }
                }
            };
            
            _xmppClient.Initialize().GetAwaiter().GetResult();
        }
    }
}