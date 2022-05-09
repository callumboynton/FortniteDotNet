using Newtonsoft.Json;

namespace FortniteDotNet.Payloads.FortniteService.Campaign
{
    public class ResearchItemFromCollectionBook
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }
    }
}