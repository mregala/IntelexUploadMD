using Newtonsoft.Json;

namespace IntelexUploadMD.Model.EntitySets
{
    //public class EHSIncidentMang_csSubLocation2ObjectSet
    //{
    //    public string Id { get; set; }
    //    public bool Deleted { get; set; }
    //    public object csInactive { get; set; }
    //    public string csName { get; set; }
    //    public string SubLocs3 { get; set; }
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class EHSIncidentMang_csSubLocation2ObjectSet
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<EHSIncidentMang_csSubLocation2ObjectSetValue> value { get; set; }
    }

    public class SubLocs3
    {
        public string Id { get; set; }
    }

    public class EHSIncidentMang_csSubLocation2ObjectSetValue
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }

        [JsonProperty("@odata.id")]
        public string odataid { get; set; }

        [JsonProperty("@odata.editLink")]
        public string odataeditLink { get; set; }
        public string Id { get; set; }
        public string csName { get; set; }
        public object csInactive { get; set; }
        public List<SubLocs3> SubLocs3 { get; set; }
    }



}
