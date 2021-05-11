using Newtonsoft.Json;

namespace FortniteDotNet.Models.XMPP.Meta
{
    public class AthenaCosmeticLoadout
    {
        [JsonProperty("AthenaCosmeticLoadout")]
        public AthenaCosmeticLoadoutData Data { get; set; }

        public AthenaCosmeticLoadout(string cid = null, string bid = null, string pid = null, params object[] scratchpad)
        {
            Data = new AthenaCosmeticLoadoutData(cid, bid, pid, scratchpad);
        }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
    
    public class AthenaCosmeticLoadoutData
    {
        [JsonProperty("characterDef")]
        public string CharacterItemDefinition { get; set; }

        [JsonProperty("characterEKey")] 
        public string CharacterEKey => "";

        [JsonProperty("backpackDef")] 
        public string BackpackItemDefinition { get; set; }

        [JsonProperty("backpackEKey")] 
        public string BackpackEKey => "";

        [JsonProperty("pickaxeDef")] 
        public string PickaxeItemDefinition { get; set; }

        [JsonProperty("pickaxeEKey")] 
        public string PickaxeEKey => "";

        [JsonProperty("contrailDef")] 
        public string ContrailItemDefinition => "/Game/Athena/Items/Cosmetics/Contrails/DefaultContrail.DefaultContrail";

        [JsonProperty("contrailEKey")] 
        public string ContrailEKey => "";
        
        [JsonProperty("scratchpad")]
        public object[] Scratchpad { get; set; }

        public AthenaCosmeticLoadoutData(string cid = null, string bid = null, string pid = null, params object[] scratchpad)
        {
            CharacterItemDefinition = cid == null ? 
                "/Game/Athena/Items/Cosmetics/Characters/CID_001_Athena_Commando_F_Default.CID_001_Athena_Commando_F_Default" : 
                $"/Game/Athena/Items/Cosmetics/Characters/{cid}.{cid}";
            BackpackItemDefinition = bid == null ? "None" : $"/Game/Athena/Items/Cosmetics/Backpacks/{bid}.{bid}";
            PickaxeItemDefinition = pid == null ? 
                "/Game/Athena/Items/Cosmetics/Pickaxes/DefaultPickaxe.DefaultPickaxe" : 
                $"/Game/Athena/Items/Cosmetics/Pickaxes/{pid}.{pid}";
            Scratchpad = scratchpad;
        }
    }
}