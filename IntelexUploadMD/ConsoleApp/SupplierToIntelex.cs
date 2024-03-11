using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Responses;
using IntelexUploadMD.Services.ExtractHelper.DataExtractHelper;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using IntelexUploadMD.Services.PayloadBuilder;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace IntelexUploadMD.ConsoleApp
{
    public class SupplierToIntelex
    {
        private readonly ISupplierExtractHelper _dataExtractHelper;
        private readonly IConfiguration _configuration;
        //private string? locationId;
        //private string? sublocation2Id;
        //private SupplierExtractHelper supplierExtractHelper;

        //public SupplierToIntelex(SupplierExtractHelper supplierExtractHelper)
        //{
        //    this.supplierExtractHelper = supplierExtractHelper;
        //}

        public SupplierToIntelex(
            ISupplierExtractHelper dataExtractHelper, IConfiguration configuration)
        {
            _dataExtractHelper = dataExtractHelper;
            _configuration = configuration;
        }

        public async Task SupplierToIntelexFunc(/*ISubLocation1ExtractHelper _dataExtractHelper*/)
        {
            /// Load configuration from JSON file
            //var config = ConfigHelper.LoadConfig("local.settings.json");

            /// Instantiate API method classes usage
            PostDataToAPI postToApi = new PostDataToAPI();
            DeleteDataToAPI deleteToApi = new DeleteDataToAPI();
            PatchDataToAPI updateToApi = new PatchDataToAPI();

            if (_configuration != null)
            {
                // Use configuration values
                string excelFilePath = _configuration["ExcelFilePath"];
                string supplierApiEndpoint = _configuration["SupplierApiEndpoint"];
                string intelexURL = _configuration["IntelexURLPreProd"];
                string TargetWorksheetNameSubLocation1 = _configuration["TargetWorksheetNameSupplier"];
                string apiUsername = _configuration["ApiUsername"];
                string apiPassword = _configuration["ApiPassword"];

                try
                {
                    var excelPackage = new ExcelPackage(new FileInfo(excelFilePath));

                    // Find the target worksheet by name
                    var worksheet = excelPackage.Workbook.Worksheets[TargetWorksheetNameSubLocation1];

                    if (worksheet != null)//if (excelPackage.Workbook.Worksheets.Count > 0)
                    {
                        // Assuming your data starts from the second row (skip header)
                        int startRow = 2;
                        int ctr = 1;

                        while (worksheet.Cells[startRow, 1].Value != null)
                        {
                            // Generate a GUID for each row
                            Guid rowGuid = Guid.NewGuid();
                            // Get data from Excel
                            string type = worksheet.Cells[startRow, 1].Text;
                            string newSupplierNumber = worksheet.Cells[startRow, 2].Text;
                            string oldSupplierNumber = worksheet.Cells[startRow, 3].Text;
                            string newName = worksheet.Cells[startRow, 4].Text;
                            string Inactive = worksheet.Cells[startRow, 5].Text;
                            string OldILXName = worksheet.Cells[startRow, 7].Text;
                            // Add more columns as needed       

                            var suppApiEndpoint = intelexURL + supplierApiEndpoint; //endpoint for Supplier(Intelex)
                            string truncSupplierName = newName;//.Substring(0, Math.Min(5, Name.Length));  //truncated newName for Supplier(Intelex)
                            string truncSupplierOldILXName = OldILXName;//.Substring(0, Math.Min(5, Name.Length));  //truncated newName for Supplier(Intelex)

                            //// Query fields for each entity for Sublocation1
                            var supplierDataSet = await _dataExtractHelper.GetSupplierDataSet(suppApiEndpoint, type, truncSupplierOldILXName, apiUsername, apiPassword, ctr, newSupplierNumber);
                            //// Check PARENT Id of SubLocation1 - Location
                            //var parentSupplierDataSet = await _dataExtractHelper.GetParentSupplierDataSet(parentSubLoc1ApiEndpoint, type, Parent, Parent, apiUsername, apiPassword);

                            //// Check CHILD Id of SubLocation1 - Sublocation2
                            //var childSupplierDataSet = await _dataExtractHelper.GetChildSupplierDataSet(childSubLoc1ApiEndpoint, type, truncatedSupplierName, Parent, apiUsername, apiPassword);

                            ////Get Parent ID for EHSIncidentMang_csSubLocation1ObjectSet - SysLocationEntitySet
                            //if (parentSupplierDataSet.SysLocationEntitySetResult != null && parentSupplierDataSet.SysLocationEntitySetResult.value.Count > 0) //EXISTS parent!!
                            //{
                            //    // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                            //    var locationValues = parentSupplierDataSet.SysLocationEntitySetResult.value;

                            //    // Check if supplierValues is not null and contains any elements
                            //    if (locationValues != null && locationValues.Any())
                            //    {
                            //        // Loop through each item in supplierValues
                            //        foreach (var item in locationValues)
                            //        {
                            //            locationId = item.Id;
                            //            Console.WriteLine($"Location Id: {locationId}");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine("No data found in SysLocationEntitySetResult.value");
                            //    }
                            //}
                            //else
                            //{
                            //    Console.WriteLine("SysLocationEntitySetResult is null");
                            //}


                            ///// CHILD!!!
                            //if (childSupplierDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult != null && childSupplierDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.Count > 0) //EXISTS parent!!
                            //{
                            //    // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                            //    var subLocation2Values = childSupplierDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value;

                            //    // Check if supplierValues is not null and contains any elements
                            //    if (subLocation2Values != null && subLocation2Values.Any())
                            //    {
                            //        // Loop through each item in supplierValues
                            //        foreach (var item in subLocation2Values)
                            //        {
                            //            sublocation2Id = item.Id;
                            //            Console.WriteLine($"SubLocation2 Id: {sublocation2Id}");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine("No data found in EHSIncidentMang_csSubLocation2ObjectSetResult.value");
                            //    }
                            //}
                            //else
                            //{
                            //    Console.WriteLine("EHSIncidentMang_csSubLocation2ObjectSetResult is null");
                            //}


                            /// Check if EHSIncidentMang_ContractorObjectSetResult is not null
                            if (supplierDataSet.EHSIncidentMang_ContractorObjectSetResult != null && supplierDataSet.EHSIncidentMang_ContractorObjectSetResult.value.Count > 0) // EXIST!! so UPDATE!!
                            {
                                Console.WriteLine($"Bulk Sybnc for Supplier MD to Intelex....\n");
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var supplierValues = supplierDataSet.EHSIncidentMang_ContractorObjectSetResult.value;
                                var supp_ctr = 1;
                                // Check if supplierValues is not null and contains any elements
                                if (supplierValues != null && supplierValues.Any() && supp_ctr <= supplierValues.Count)
                                {
                                    // Loop through each item in supplierValues
                                    foreach (var item in supplierValues)
                                    {
                                        // Access the Id property of each item
                                        var supplierId = item.Id;
                                        var supplierName = item.Name;
                                        var supplierInactive = item.Inactive;

                                        // Do something with subLocationId
                                        Console.WriteLine($"Updating Supplier MD with Supplier Id: {supplierId}");

                                        ////Payload Builder for Update
                                        ///
                                        //var payloadUpdate = assetDataSet.BuildSubLocation2Payload_Update(subLocation1Id, Name);
                                        //var jsonPayload = JsonConvert.SerializeObject(payloadUpdate, Formatting.Indented);
                                        //var response = PatchDataToApi($"{subLoc2ApiEndpoint}({subLocation2Values.FirstOrDefault().Id})", payloadUpdate, apiUsername, apiPassword);

                                        Console.WriteLine($"Updating Supplier MD: {ctr} : '{newSupplierNumber}' to New Name '{newName}' to Intelex....");
                                        var payloadUpdate = SupplierPayloadBuilderHelper.BuildSupplierPayload_Update(supplierId, Inactive, newName, newSupplierNumber);
                                        var jsonPayload = JsonConvert.SerializeObject(payloadUpdate, Formatting.Indented);
                                        //Console.WriteLine($"Updating for Supplier MD: '{supp_ctr}' : '{supplierName}' to Intelex....");
                                        var response = updateToApi.PatchDataToApi($"{suppApiEndpoint}({supplierId})", payloadUpdate, apiUsername, apiPassword);
                                        supp_ctr++;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No data found in EHSIncidentMang_ContractorObjectSetResult.value\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("EHSIncidentMang_ContractorObjectSetResult is null");

                                Console.WriteLine($"Rechecking if Supplier: {ctr} : with Supplier Name: '{truncSupplierName}' Already Exits in Intelex....");
                                supplierDataSet = await _dataExtractHelper.GetSupplierDataSet(suppApiEndpoint, "dummy", truncSupplierName, apiUsername, apiPassword, ctr, newSupplierNumber);

                                if (supplierDataSet.EHSIncidentMang_ContractorObjectSetResult != null && supplierDataSet.EHSIncidentMang_ContractorObjectSetResult.value.Count > 0)
                                {
                                    Console.WriteLine($"Supplier: {ctr} : with Supplier Name: '{truncSupplierName}' ALready Exits in Intelex!!!\n");
                                }
                                else
                                {
                                    ////Payload Builder for Create/Post
                                    Console.WriteLine($"Creating New Supplier MD: {ctr} : '{newSupplierNumber}' : '{newName}' to Intelex....");
                                    var payloadCreate = SupplierPayloadBuilderHelper.BuildSupplierPayload_Create(rowGuid.ToString(), Inactive/*bool.TryParse(Inactive, out bool result)*/, newName, newSupplierNumber);
                                    var jsonPayload = JsonConvert.SerializeObject(payloadCreate, Formatting.Indented);
                                    var response = postToApi.PostDataToApi(suppApiEndpoint, payloadCreate, apiUsername, apiPassword);
                                }
                            }
                            ctr++;
                            startRow++;
                        }

                        Console.WriteLine("Data sync for Supplier MD to Intelex completed successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Worksheet '{TargetWorksheetNameSubLocation1}' not found in the Excel file.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Failed to load configuration from the JSON file.");
            }
        }
    }
}

