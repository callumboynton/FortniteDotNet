using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Models.Party;
using FortniteDotNet.Models.XMPP.Payloads;
using FortniteDotNet.Models.XMPP.EventArgs;

namespace FortniteDotNet.Models.XMPP
{
    public sealed partial class XMPPClient : IAsyncDisposable
    {
        public string Jid { get; set; }
        public string Resource { get; set; }
        public OAuthSession AuthSession { get; set; }
        public ClientWebSocket XmppClient { get; set; }
        
        public Presence LastPresence { get; set; }
        public PartyInfo CurrentParty { get; set; }
        public List<string> KickedPartyIds { get; set; }
        
        public XmlWriterSettings WriterSettings => new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Async = true
        };

        public XMPPClient(OAuthSession oAuthSession)
        {
            AuthSession = oAuthSession;
            
            Jid = $"{AuthSession.AccountId}@prod.ol.epicgames.com";
            Resource = $"V2:Fortnite:WINDOWS::{Guid.NewGuid()}";

            XmppClient = new ClientWebSocket();
            XmppClient.Options.AddSubProtocol("xmpp");
            XmppClient.Options.Credentials = new NetworkCredential
            {
                UserName = Jid,
                Password = AuthSession.AccessToken
            };
            XmppClient.Options.KeepAliveInterval = TimeSpan.FromSeconds(60);

            KickedPartyIds = new();
        }
        
        public async Task Initialize()
        {
            await XmppClient.ConnectAsync(new Uri("wss://xmpp-service-prod.ol.epicgames.com//"), CancellationToken.None);
            await SendOpen();
            await HandleMessages();
        }
        
        public async Task HandleMessages()
        {
            var memoryStream = new MemoryStream();

            while (XmppClient.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;
                do
                {
                    var messageBuffer = WebSocket.CreateClientBuffer(1024, 16);
                    result = await XmppClient.ReceiveAsync(messageBuffer, CancellationToken.None);
                    memoryStream.Write(messageBuffer.Array, messageBuffer.Offset, result.Count);
                } while (!result.EndOfMessage);

                switch (result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await DisposeAsync();
                        break;
                    case WebSocketMessageType.Text:
                    {
                        var message = Encoding.UTF8.GetString(memoryStream.ToArray());
                
                        var document = new XmlDocument();
                        try { document.LoadXml(message.Trim()); }
                        catch { return; }

                        switch (document.DocumentElement.Name)
                        {
                            case "stream:features":
                                await SendAuth();
                                break;
                            case "success":
                                await SendIq("_xmpp_bind1");
                                break;
                            case "iq":
                                if (document.DocumentElement.GetAttribute("id") == "_xmpp_bind1")
                                    await SendIq("_xmpp_session1");
                                if (document.DocumentElement.GetAttribute("id") == "_xmpp_session1")
                                    await SendPresence(LastPresence ?? new Presence());
                                break;
                            case "message":
                                await HandleMessage(document);
                                break;
                        }
                
                        var args = new MessageEventArgs
                        {
                            MessageType = document.DocumentElement.Name,
                            Document = document
                        };
                        onMessageReceived(args);
                        break;
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.Position = 0;
                memoryStream.SetLength(0);
            }
        }

        public async Task SendAsync(string data)
            => await XmppClient.SendAsync(new(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);        

        #region Event Handlers
        
        public event MessageEventHandler OnMessageReceived;
        private void onMessageReceived(MessageEventArgs e)
            => OnMessageReceived?.Invoke(this, e);
        
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public event ChatEventHandler OnChatReceived;
        private void onChatReceived(ChatEventArgs e)
            => OnChatReceived?.Invoke(this, e);
        
        public delegate void ChatEventHandler(object sender, ChatEventArgs e);
        
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
        
        public event PartyInviteEventHandler OnPartyInvite;
        private void onPartyInvite(PartyInvite e)
            => OnPartyInvite?.Invoke(this, e);
        
        public delegate void PartyInviteEventHandler(object sender, PartyInvite e);
        
        public event PartyMemberEventHandler OnPartyMemberJoined;
        private void onPartyMemberJoined(PartyMember e)
            => OnPartyMemberJoined?.Invoke(this, e);
        
        public delegate void PartyMemberEventHandler(object sender, PartyMember e);
        
        
        #endregion

        public async ValueTask DisposeAsync()
        {
            await XmppClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "unavailable", CancellationToken.None);
            XmppClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}