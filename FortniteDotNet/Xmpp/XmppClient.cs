using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using FortniteDotNet.Models.AccountService;
using FortniteDotNet.Models.PartyService;
using FortniteDotNet.Services;
using FortniteDotNet.Xmpp.EventArgs;
using FortniteDotNet.Xmpp.Payloads;
using Microsoft.Extensions.DependencyInjection;

namespace FortniteDotNet.Xmpp
{
    public sealed partial class XmppClient
    {
        private readonly IPartyService _partyService;
        private readonly IFriendsService _friendsService;
        
        internal string JabberId { get; set; }
        internal string Resource { get; set; }
        
        private readonly ClientWebSocket _client;
        private readonly XmlWriterSettings _writerSettings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true
        };
        
        internal AuthSession AuthSession { get; }
        
        internal List<string> KickedPartyIds { get; }
        public Party CurrentParty { get; internal set; }
        
        public Presence LastPresence { get; internal set; }
        
        public XmppClient(IServiceProvider services, AuthSession authSession)
        {
            AuthSession = authSession;
            
            _partyService = services.GetService<IPartyService>();
            _friendsService = services.GetService<IFriendsService>();
            
            JabberId = $"{authSession.AccountId}@prod.ol.epicgames.com";
            Resource = $"V2:Fortnite:WIN::{Guid.NewGuid().ToString().Replace("-", "").ToUpper()}";

            _client = new ClientWebSocket();
            _client.Options.AddSubProtocol("xmpp");
            _client.Options.Credentials = new NetworkCredential
            {
                UserName = JabberId,
                Password = authSession.AccessToken
            };
            _client.Options.KeepAliveInterval = TimeSpan.FromSeconds(60);

            KickedPartyIds = new List<string>();
        }

        public async Task Connect()
        {
            await _client.ConnectAsync(new Uri("wss://xmpp-service-prod.ol.epicgames.com/"), CancellationToken.None);
            await SendOpen();
            await HandleMessages();
        }
        
        private async Task HandleMessages()
        {
            var memoryStream = new MemoryStream();
            var buffer = WebSocket.CreateClientBuffer(2048, 2048);

            while (_client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;
                do
                {
                    result = await _client.ReceiveAsync(buffer, CancellationToken.None);
                    memoryStream.Write(buffer.Array, buffer.Offset, result.Count);
                } while (!result.EndOfMessage);

                switch (result.MessageType)
                {
                    case WebSocketMessageType.Text:
                    {
                        var message = Encoding.UTF8.GetString(memoryStream.ToArray());
                        var document = new XmlDocument();
                        try
                        {
                            document.LoadXml(message.Trim());
                        }
                        catch
                        {
                            return;
                        }

                        switch (document.DocumentElement.Name)
                        {
                            case "stream:features":
                                await SendAuth();
                                break;
                            case "success":
                                await SendIq("_xmpp_bind1");
                                onReady();
                                break;
                            case "iq":
                                if (document.DocumentElement.GetAttribute("id") == "_xmpp_bind1")
                                    await SendIq("_xmpp_session1");
                                if (document.DocumentElement.GetAttribute("id") == "_xmpp_session1")
                                    await SendPresence(LastPresence ?? new Presence());
                                break;
                            case "message":
                                await HandleMessage(document.DocumentElement);
                                break;
                            case "presence":
                                HandlePresence(document.DocumentElement);
                                break;
                        }

                        onMessageReceived(new MessageEventArgs(document.DocumentElement));
                        break;
                    }
                }

                memoryStream = new MemoryStream();
            }
        }
        
        private async Task SendAsync(string data)
            => await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);
        
        public async Task InitParty()
        {
            // Get the party summary of the account bound to the provided XMPP client, and update its current party.
            var summary = await _partyService.GetSummary(AuthSession);
            CurrentParty = summary.Current.FirstOrDefault();
            
            if (CurrentParty != null)
                await LeaveParty();
            
            CurrentParty ??= await _partyService.CreateParty(AuthSession, new CreationInfo(this));
        }

        public async Task LeaveParty()
        {
            await LeavePartyChat();
            await _partyService.RemoveMember(AuthSession, CurrentParty.Id, AuthSession.AccountId);
            CurrentParty = null;
        }

        public event EventHandler OnReady;
        private void onReady()
            => OnReady?.Invoke(this, System.EventArgs.Empty);
        
        public event MessageEventHandler OnMessageReceived;
        private void onMessageReceived(MessageEventArgs e)
            => OnMessageReceived?.Invoke(this, e);
        
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        
        public event PresenceEventHandler OnPresenceReceived;
        private void onPresenceReceived(PresenceEventArgs e)
            => OnPresenceReceived?.Invoke(this, e);
        
        public delegate void PresenceEventHandler(object sender, PresenceEventArgs e);

        public event ChatEventHandler OnChatReceived;
        private void onChatReceived(ChatEventArgs e)
            => OnChatReceived?.Invoke(this, e);
        
        public delegate void ChatEventHandler(object sender, ChatEventArgs e);

        public event GroupChatEventHandler OnGroupChatReceived;
        private void onGroupChatReceived(GroupChatEventArgs e)
            => OnGroupChatReceived?.Invoke(this, e);
        
        public delegate void GroupChatEventHandler(object sender, GroupChatEventArgs e);
        
        public event FriendEventHandler OnFriendRequestSent;
        private void onFriendRequestSent(Friend e)
            => OnFriendRequestSent?.Invoke(this, e);
        
        public event FriendEventHandler OnFriendRequestReceived;
        private void onFriendRequestReceived(Friend e)
            => OnFriendRequestReceived?.Invoke(this, e);
        
        public event FriendEventHandler OnFriendRequestAccepted;
        private void onFriendRequestAccepted(Friend e)
            => OnFriendRequestAccepted?.Invoke(this, e);
        
        public delegate void FriendEventHandler(object sender, Friend e);
        
        public event FriendRemovedEventHandler OnFriendshipRemoved;
        private void onFriendshipRemoved(FriendRemoval e)
            => OnFriendshipRemoved?.Invoke(this, e);
        
        public delegate void FriendRemovedEventHandler(object sender, FriendRemoval e);
        
        public event BlockListUpdatedEventHandler OnUserBlocked;
        private void onUserBlocked(BlockListEntry e)
            => OnUserBlocked?.Invoke(this, e);
        
        public event BlockListUpdatedEventHandler OnUserUnblocked;
        private void onUserUnblocked(BlockListEntry e)
            => OnUserUnblocked?.Invoke(this, e);
        
        public delegate void BlockListUpdatedEventHandler(object sender, BlockListEntry e);
        
        public event PartyInviteEventHandler OnPartyInviteReceived;
        private void onPartyInviteReceived(Invite e)
            => OnPartyInviteReceived?.Invoke(this, e);
        
        public delegate void PartyInviteEventHandler(object sender, Invite e);
        
        public event PartyInviteDeclinedEventHandler OnPartyInviteDeclined;
        private void onPartyInviteDeclined(Models.FriendsService.Friend e)
            => OnPartyInviteDeclined?.Invoke(this, e);
        
        public delegate void PartyInviteDeclinedEventHandler(object sender, Models.FriendsService.Friend e);
        
        public event PartyUpdatedEventHandler OnPartyUpdated;
        private void onPartyUpdated(PartyUpdatedEventArgs e)
            => OnPartyUpdated?.Invoke(this, e);
        
        public delegate void PartyUpdatedEventHandler(object sender, PartyUpdatedEventArgs e);
        
        public event PartyMemberUpdatedEventHandler OnPartyMemberUpdated;
        private void onPartyMemberUpdated(PartyMemberUpdatedEventArgs e)
            => OnPartyMemberUpdated?.Invoke(this, e);
        
        public delegate void PartyMemberUpdatedEventHandler(object sender, PartyMemberUpdatedEventArgs e);
        
        public event PartyMemberEventHandler OnPartyMemberJoined;
        private void onPartyMemberJoined(Member e)
            => OnPartyMemberJoined?.Invoke(this, e);
        
        public event PartyMemberEventHandler OnPartyMemberPromoted;
        private void onPartyMemberPromoted(Member e)
            => OnPartyMemberPromoted?.Invoke(this, e);
        
        public event PartyMemberEventHandler OnPartyMemberLeft;
        private void onPartyMemberLeft(Member e)
            => OnPartyMemberLeft?.Invoke(this, e);

        public event PartyMemberEventHandler OnPartyMemberExpired;
        private void onPartyMemberExpired(Member e)
            => OnPartyMemberExpired?.Invoke(this, e);

        public event PartyMemberEventHandler OnPartyMemberKicked;
        private void onPartyMemberKicked(Member e)
            => OnPartyMemberKicked?.Invoke(this, e);

        public event PartyMemberEventHandler OnPartyMemberDisconnected;
        private void onPartyMemberDisconnected(Member e)
            => OnPartyMemberDisconnected?.Invoke(this, e);
        
        public delegate void PartyMemberEventHandler(object sender, Member e);
        
    }
}