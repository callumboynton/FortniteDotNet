using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class AthenaBannerInfo
    {
        [JsonProperty("AthenaBannerInfo")]
        public AthenaBannerInfoData Data { get; set; }

        public AthenaBannerInfo(string bannerIconId, string bannerColorId, int seasonLevel)
        {
            Data = new AthenaBannerInfoData(bannerIconId, bannerColorId, seasonLevel);
        }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class AthenaBannerInfoData
    {
        [JsonProperty("bannerIconId")]
        public string BannerIconId { get; set; }
        
        [JsonProperty("bannerColorId")]
        public string BannerColorId { get; set; }
        
        [JsonProperty("seasonLevel")]
        public int SeasonLevel { get; set; }

        public AthenaBannerInfoData(string bannerIconId, string bannerColorId, int seasonLevel)
        {
            BannerIconId = bannerIconId;
            BannerColorId = bannerColorId;
            SeasonLevel = seasonLevel;
        }
    }
}