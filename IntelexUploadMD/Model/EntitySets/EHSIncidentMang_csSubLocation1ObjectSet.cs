using Newtonsoft.Json;

namespace IntelexUploadMD.Model.EntitySets
{

    public class CsMainLocation
    {
        public string Id { get; set; }
    }

    public class CsSubLocs2
    {
        public string Id { get; set; }
    }

    public class EHSIncidentMang_csSubLocation1ObjectSet
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<EHSIncidentMang_csSubLocation1ObjectSetValue> value { get; set; }
    }

    public class EHSIncidentMang_csSubLocation1ObjectSetValue
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }

        [JsonProperty("@odata.id")]
        public string odataid { get; set; }

        [JsonProperty("@odata.editLink")]
        public string odataeditLink { get; set; }
        public string Id { get; set; }
        //public bool Deleted { get; set; }
        public string csName { get; set; }
        public object csInactive { get; set; }
        public List<CsMainLocation> csMainLocation { get; set; }
        public List<CsSubLocs2> csSubLocs2 { get; set; }
    }

}
