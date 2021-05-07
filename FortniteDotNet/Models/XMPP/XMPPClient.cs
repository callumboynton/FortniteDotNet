using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Models.XMPP
{
    public sealed partial class XMPPClient : IAsyncDisposable
    {
        public string Jid { get; set; }
        public string Resource { get; set; }
        public Presence LastPresence { get; set; }
        public OAuthSession AuthSession { get; set; }
        public ClientWebSocket XmppClient { get; set; }
        
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
        }
        
        public async Task Initialize()
        {
            await XmppClient.ConnectAsync(new Uri("wss://xmpp-service-prod.ol.epicgames.com//"), CancellationToken.None).ConfigureAwait(false);
            await SendOpen().ConfigureAwait(false);
            await HandleMessages().ConfigureAwait(false);
        }
        
        public async Task HandleMessages()
        {
            var memoryStream = new MemoryStream();

                while (XmppClient.State == WebSocketState.Open)
                {
                    var buffer = WebSocket.CreateClientBuffer(1024, 16);
                    var result = await XmppClient.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                    while (!result.EndOfMessage)
                        memoryStream.Write(buffer.Array, buffer.Offset, result.Count);

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
                                    await SendAuth().ConfigureAwait(false);
                                    break;
                                case "success":
                                    await SendIq("_xmpp_bind1").ConfigureAwait(false);
                                    break;
                                case "iq":
                                    if (document.DocumentElement.GetAttribute("id") == "_xmpp_bind1")
                                        await SendIq("_xmpp_session1").ConfigureAwait(false);
                                    break;
                            }
                    
                            var args = new MessageEventArgs
                            {
                                MessageType = document.DocumentElement.Name,
                                Document = document
                            };
                            OnMessageReceived(args);
                            break;
                        }
                    }

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.Position = 0;
                    memoryStream.SetLength(0);
                }
        }

        public async Task SendAsync(string data)
            => await XmppClient.SendAsync(new(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);        

        public event MessageEventHandler OnMessage;
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        private void OnMessageReceived(MessageEventArgs e)
            => OnMessage?.Invoke(this, e);

        public async ValueTask DisposeAsync()
        {
            await SendPresence(LastPresence, false).ConfigureAwait(false);
            await XmppClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "unavailable", CancellationToken.None).ConfigureAwait(false);
            XmppClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}