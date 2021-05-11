using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class BattlePassInfo
    {
        [JsonProperty("BattlePassInfo")]
        public BattlePassInfoData Data { get; set; }

        public BattlePassInfo(int passLevel, int selfBoostXp, int friendXpBoost)
        {
            Data = new BattlePassInfoData(passLevel, selfBoostXp, friendXpBoost);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class BattlePassInfoData
    {
        [JsonProperty("bHasPurchasedPass")] 
        public bool HasPurchasedPass => true;
        
        [JsonProperty("passLevel")]
        public int PassLevel { get; set; }
        
        [JsonProperty("selfBoostXp")]
        public int PersonalXpBoost { get; set; }
        
        [JsonProperty("friendBoostXp")]
        public int FriendXpBoost { get; set; }

        public BattlePassInfoData(int passLevel, int selfBoostXp, int friendXpBoost)
        {
            PassLevel = passLevel;
            PersonalXpBoost = selfBoostXp;
            FriendXpBoost = friendXpBoost;
        }
    }
}