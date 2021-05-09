using FortniteDotNet.Enums.Party;

namespace FortniteDotNet.Models.Party
{
    public class PartyPrivacy
    {
        public string PartyType { get; set; }
        public string InviteRestriction { get; set; }
        public bool OnlyLeaderFriendsCanJoin { get; set; }
        public string PresencePermission { get; set; }
        public string InvitePermission { get; set; }
        public bool AcceptingMembers { get; set; }

        public PartyPrivacy(Privacy privacy)
        {
            switch (privacy)
            {
                case Privacy.Public:
                {
                    PartyType = "Public";
                    InviteRestriction = "AnyMember";
                    OnlyLeaderFriendsCanJoin = false;
                    PresencePermission = "Anyone";
                    InvitePermission = "Anyone";
                    AcceptingMembers = true;
                    break;
                }
                case Privacy.Friends:
                {
                    PartyType = "FriendsOnly";
                    InviteRestriction = "LeaderOnly";
                    OnlyLeaderFriendsCanJoin = true;
                    PresencePermission = "Leader";
                    InvitePermission = "Leader";
                    AcceptingMembers = false;
                    break;
                }
                case Privacy.FriendsAllowFriendsOfFriends:
                {
                    PartyType = "FriendsOnly";
                    InviteRestriction = "AnyMember";
                    OnlyLeaderFriendsCanJoin = false;
                    PresencePermission = "Anyone";
                    InvitePermission = "AnyMember";
                    AcceptingMembers = true;
                    break;
                }
                case Privacy.Private:
                {
                    PartyType = "Private";
                    InviteRestriction = "LeaderOnly";
                    OnlyLeaderFriendsCanJoin = true;
                    PresencePermission = "Noone";
                    InvitePermission = "Leader";
                    AcceptingMembers = false;
                    break;
                }
                case Privacy.PrivateAllowFriendsOfFriends:
                {
                    PartyType = "Private";
                    InviteRestriction = "AnyMember";
                    OnlyLeaderFriendsCanJoin = false;
                    PresencePermission = "Noone";
                    InvitePermission = "AnyMember";
                    AcceptingMembers = false;
                    break;
                }
            }
        }
    }
}