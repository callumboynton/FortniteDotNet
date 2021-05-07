using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using FortniteDotNet.Models.Accounts;

namespace FortniteDotNet.Models.XMPP
{
    public sealed partial class XMPPClient
    {
        public Guid Guid { get; set; }
        public string Jid { get; set; }
        public string Resource { get; set; }
        public Presence LastPresence { get; set; }
        public ClientWebSocket XmppClient { get; set; }
        public OAuthSession AuthSession { get; set; }
        
        public XmlWriterSettings WriterSettings => new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Async = true
        };

        public XMPPClient(OAuthSession oAuthSession)
        {
            AuthSession = oAuthSession;
            
            Guid = Guid.NewGuid();
            Jid = $"{AuthSession.AccountId}@prod.ol.epicgames.com";
            Resource = $"V2:Fortnite:WINDOWS::{Guid}";

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
                    }
                    while (!result.EndOfMessage);

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Close:
                            await XmppClient.CloseAsync(WebSocketCloseStatus.NormalClosure, GetPresenceAsXml("", "", LastPresence, true), CancellationToken.None);
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
                                        await SendPresence(new Presence
                                        {
                                            Status = "deez nuts",
                                            IsJoinable = false,
                                            IsPlaying = false,
                                            HasVoiceSupport = false,
                                            Properties = new(),
                                            SessionId = ""
                                        });
                                    break;
                            }
                    
                            var args = new MessageEventArgs
                            {
                                MessageType = document.DocumentElement.Name,
                                Data = document
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

        public string GetPresenceAsXml(string to, string from, Presence presence, bool isUnavailablePresence = false)
        {
            var builder = new StringBuilder();
            var writer = XmlWriter.Create(builder, WriterSettings);

            writer.WriteStartElement("presence", "jabber:client");
            {
                writer.WriteAttributeString("to", to);
                writer.WriteAttributeString("from", from);
                if (isUnavailablePresence)
                    writer.WriteAttributeString("type", "unavailable");

                writer.WriteStartElement("status");
                {
                    writer.WriteString(JsonConvert.SerializeObject(presence));
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();

            return builder.ToString();
        }

        public async Task SendMessage(string data)
            => await XmppClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);        

        public event MessageEventHandler OnMessage;
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        private void OnMessageReceived(MessageEventArgs e)
            => OnMessage?.Invoke(this, e);
    }
}