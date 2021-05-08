using System;
using System.Xml;
using Newtonsoft.Json;
using FortniteDotNet.Models.XMPP.Payloads;
using FortniteDotNet.Models.XMPP.EventArgs;

namespace FortniteDotNet.Models.XMPP
{
    public partial class XMPPClient
    {
        public void HandleMessage(XmlDocument document)
        {
            switch (document.DocumentElement.GetAttribute("type"))
            {
                case "chat":
                    HandleChat(document);
                    break;
                case "error":
                case "headline":
                case "groupchat":
                    return;
            }

            var body = JsonConvert.DeserializeObject<dynamic>(document.DocumentElement.InnerText);
            switch (body.type.ToString())
            {
                case "com.epicgames.friends.core.apiobjects.Friend":
                {
                    var payload = JsonConvert.DeserializeObject<Friend>((string)body.payload.ToString());
                    if (payload.Status == "ACCEPTED")
                        onFriendRequestAccepted(payload);
                    else if (payload.Direction == "INBOUND")
                        onFriendRequestReceived(payload);
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.FriendRemoval":
                {
                    var payload = body.payload as FriendRemoval;
                    onFriendshipRemoved(payload);
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.BlockListEntryAdded":
                {
                    var payload = JsonConvert.DeserializeObject<BlockListEntry>((string)body.payload.ToString());
                    onUserBlocked(payload);
                    break;
                }
                case "com.epicgames.friends.core.apiobjects.BlockListEntryRemoved":
                {
                    var payload = JsonConvert.DeserializeObject<BlockListEntry>((string)body.payload.ToString());
                    onUserUnblocked(payload);
                    break;
                }
                case "com.epicgames.social.party.notification.v0.PING":
                case "com.epicgames.social.party.notification.v0.MEMBER_JOINED":
                case "com.epicgames.social.party.notification.v0.MEMBER_STATE_UPDATED":
                case "com.epicgames.social.party.notification.v0.MEMBER_LEFT":
                case "com.epicgames.social.party.notification.v0.MEMBER_EXPIRED":
                case "com.epicgames.social.party.notification.v0.MEMBER_KICKED":
                case "com.epicgames.social.party.notification.v0.MEMBER_DISCONNECTED":
                case "com.epicgames.social.party.notification.v0.MEMBER_NEW_CAPTAIN":
                case "com.epicgames.social.party.notification.v0.PARTY_UPDATED":
                case "com.epicgames.social.party.notification.v0.MEMBER_REQUIRE_CONFIRMATION":
                case "com.epicgames.social.party.notification.v0.INVITE_DECLINED":
                {
                    break;
                }
                default:
                    Console.WriteLine(body.type);
                    break;
            }
        }

        public void HandleChat(XmlDocument document)
        {
            onChatReceived(new ChatEventArgs
            {
                From = document.DocumentElement.GetAttribute("from"),
                Body = document.DocumentElement.InnerText
            });
        }
    }
}