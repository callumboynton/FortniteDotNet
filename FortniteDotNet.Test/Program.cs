
using System;
using System.Threading;
using System.Threading.Tasks;
using FortniteDotNet.Models.XMPP;
using FortniteDotNet.Enums.Accounts;
using FortniteDotNet.Enums.Party;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Services;

namespace FortniteDotNet.Test
{
    internal class Program
    {
        private static OAuthSession _authSession;
        private static XMPPClient _xmppClient;
        
        private static void Main()
        {
            var api = new FortniteApi();
            Task.Run(async () =>
            {
                _authSession = await api.AccountService.GenerateOAuthSession(GrantType.DeviceAuth, AuthClient.iOS, new()
                {
                    /*{ "device_id", "03fabe92f86844a6971ea7ff55cc01a6" },
                    { "account_id", "8d89cb32d31147f28e0872116945c376" },
                    { "secret", "" }*/
                    { "device_id", "75041eacfb7a4e57a7d30dbc59f68fef" },
                    { "account_id", "d6681ac9e07344e9aeee7562b762d40d" },
                    { "secret", "" }
                });
                
                var thread = new Thread(Xmpp);
                thread.Start();
            
                Thread.Sleep(1000);
            
                var party = await _authSession.CreateParty(_xmppClient);
                await party.UpdatePrivacy(_authSession, new PartyPrivacy(Privacy.Public));
                await _xmppClient.SendMessage("Hey bitch", "8d89cb32d31147f28e0872116945c376@prod.ol.epicgames.com");

            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void Xmpp()
        {
            _xmppClient = new XMPPClient(_authSession);
            _xmppClient.OnPartyInvite += async (_, args) =>
            {
                Console.WriteLine($"Invite to party {args.PartyId} received from {args.SentBy}@prod.ol.epicgames.com");
                await _xmppClient.SendMessage($"Thanks for inviting me to your party, {args.Meta["urn:epic:member:dn_s"]}!", $"{args.SentBy}@prod.ol.epicgames.com");
                await _authSession.JoinParty(_xmppClient, args);
            };
            _xmppClient.OnChatReceived += async (_, args) =>
            {
                await _xmppClient.SendMessage(args.Body, args.From);
                Console.WriteLine(_xmppClient.CurrentParty.Members.Count);
            };
            
            
            _xmppClient.Initialize().GetAwaiter().GetResult();
        }
    }
}