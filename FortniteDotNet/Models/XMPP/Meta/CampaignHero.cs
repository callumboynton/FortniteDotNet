using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class CampaignHero
    {
        [JsonProperty("CampaignHero")]
        public CampaignHeroData Data { get; set; }
        
        public CampaignHero(string cid)
        {
            Data = new CampaignHeroData(cid);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class CampaignHeroData
    {
        [JsonProperty("heroItemInstanceId")] 
        public string HeroItemInstanceId => "";
        
        [JsonProperty("heroType")]
        public string HeroType { get; set; }

        public CampaignHeroData(string cid)
        {
            HeroType = $"/Game/Athena/Heroes/{cid}.{cid}";
        }
    }
}