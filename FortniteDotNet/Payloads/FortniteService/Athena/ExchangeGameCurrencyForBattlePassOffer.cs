using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Athena
{
    public class ExchangeGameCurrencyForBattlePassOffer
    {
        [JsonProperty("offerItemIdList")]
        public List<string> OfferItemIdList { get; set; }
    }
}