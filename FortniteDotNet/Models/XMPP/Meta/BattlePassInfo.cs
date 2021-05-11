using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class BattlePassInfo
    {
        [JsonProperty("BattlePassInfo")]
        public BattlePassInfoData Data { get; set; }

        public BattlePassInfo(bool isPurchased, int passLevel, int selfBoostXp, int friendXpBoost)
        {
            Data = new BattlePassInfoData(isPurchased, passLevel, selfBoostXp, friendXpBoost);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class BattlePassInfoData
    {
        [JsonProperty("bHasPurchasedPass")] 
        public bool HasPurchasedPass { get; set; }
        
        [JsonProperty("passLevel")]
        public int PassLevel { get; set; }
        
        [JsonProperty("selfBoostXp")]
        public int PersonalXpBoost { get; set; }
        
        [JsonProperty("friendBoostXp")]
        public int FriendXpBoost { get; set; }

        public BattlePassInfoData(bool isPurchased, int passLevel, int selfBoostXp, int friendXpBoost)
        {
            HasPurchasedPass = isPurchased;
            PassLevel = passLevel;
            PersonalXpBoost = selfBoostXp;
            FriendXpBoost = friendXpBoost;
        }
    }
}