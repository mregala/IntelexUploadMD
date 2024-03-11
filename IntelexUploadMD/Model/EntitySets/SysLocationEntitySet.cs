using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IntelexUploadMD.Model.EntitySets
{
    //public class SysLocationEntitySet
    //{
    //public string Id { get; set; }
    //public string LocationCode { get; set; }
    //public string Name { get; set; }
    //public string Path { get; set; }

 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SysLocationEntitySet
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<SysLocationEntitySetValue> value { get; set; }
    }

    public class SysLocationEntitySetValue
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }

        [JsonProperty("@odata.id")]
        public string odataid { get; set; }

        [JsonProperty("@odata.editLink")]
        public string odataeditLink { get; set; }
        public string Id { get; set; }
        public string LocationCode { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }








}


