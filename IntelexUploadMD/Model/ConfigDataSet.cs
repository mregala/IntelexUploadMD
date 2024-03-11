using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelexUploadMD.Model
{
    // Define a class to represent your configuration structure
    public class ConfigDataSet
    {
        public string ExcelFilePath { get; set; }
        public string LocationApiEndpoint { get; set; }
        public string SubLocation1ApiEndpoint { get; set; }
        public string SubLocation2ApiEndpoint { get; set; }
        public string SubLocation3ApiEndpoint { get; set; }

        public string IntelexURLPreProd { get; set; }
        public string IntelexURLProd { get; set; }
        public string TargetWorksheetNameLocation { get; set; }
        public string TargetWorksheetNameSubLocation1 { get; set; }
        public string TargetWorksheetNameSubLocation2 { get; set; }
        public string TargetWorksheetNameSubLocation3 { get; set; }
        public string TargetWorksheetNameSubLocation2Delete { get; set; }
        public string TargetWorksheetNameSubLocation3Delete { get; set; }
        public string ApiUsername { get; set; }
        public string ApiPassword { get; set; }
        // Add more properties as needed
    }
}
