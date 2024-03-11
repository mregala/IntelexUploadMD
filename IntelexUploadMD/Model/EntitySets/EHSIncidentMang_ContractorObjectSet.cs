using Newtonsoft.Json;

namespace IntelexUploadMD.Model.EntitySets
{
    public class EHSIncidentMang_ContractorObjectSet
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<EHSIncidentMang_ContractorObjectSetValue> value { get; set; }
    }

    public class EHSIncidentMang_ContractorObjectSetValue
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        [JsonProperty("@odata.id")]
        public string odataid { get; set; }
        [JsonProperty("@odata.editLink")]
        public string odataeditLink { get; set; }
        public string Id { get; set; }
        public object Inactive { get; set; }
        public string Name { get; set; }
        public int RecordNo { get; set; }
        public string ContractorNo { get; set; }
    }

}
