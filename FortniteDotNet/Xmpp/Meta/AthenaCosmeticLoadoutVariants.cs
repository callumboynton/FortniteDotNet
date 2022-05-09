using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Xmpp.Meta
{
    public class AthenaCosmeticLoadoutVariants
    {
        [JsonProperty("AthenaCosmeticLoadoutVariants")]
        public AthenaCosmeticLoadoutVariantsData Data { get; set; }

        public AthenaCosmeticLoadoutVariants(string type = null, string channel = null, string variant = null)
        {
            Data = new AthenaCosmeticLoadoutVariantsData(type, channel, variant);
        }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class AthenaCosmeticLoadoutVariantsData
    {
        [JsonProperty("vL")]
        public Dictionary<string, object> VariantLoadout { get; set; }

        public AthenaCosmeticLoadoutVariantsData(string type, string channel = null, string variant = null)
        {
            if (type == null || channel == null || variant == null)
                VariantLoadout = new Dictionary<string, object>();
            else
                VariantLoadout = new Dictionary<string, object>
                {
                    {
                        type, new
                        {
                            i = new List<Variant>
                            {
                                new Variant(channel, variant)
                            }
                        }
                    }
                };
        }
    }

    public class Variant
    {
        [JsonProperty("c")]
        public string Channel { get; set; }
        
        [JsonProperty("v")]
        public string VariantName { get; set; }

        [JsonProperty("dE")] 
        public int DE => 0;

        public Variant(string channel, string variant)
        {
            Channel = channel;
            VariantName = variant;
        }
    }
}