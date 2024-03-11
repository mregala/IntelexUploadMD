using IntelexUploadMD.Responses;
using IntelexUploadMD.Services.ExtractHelper.DataExtractHelper;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace IntelexUploadMD.ConsoleApp
{
    public class SupplierToIntelexDelete
    {
        private readonly ISupplierExtractHelper _dataExtractHelper;
        private readonly IConfiguration _configuration;
        //private string? locationId;
        //private string? sublocation2Id;
        //private SupplierExtractHelper supplierExtractHelper;

        public SupplierToIntelexDelete(
            ISupplierExtractHelper dataExtractHelper, IConfiguration configuration)
        {
            _dataExtractHelper = dataExtractHelper;
            _configuration = configuration;
        }

        public async Task SupplierToIntelexFunc(/*ISubLocation1ExtractHelper _dataExtractHelper*/)
        {
            // Load configuration from JSON file
            //var config = ConfigHelper.LoadConfig("local.settings.json");s

            /// Instantiate API method classes usage
            //PostDataToAPI postToApi = new PostDataToAPI();
            DeleteDataToAPI deleteToApi = new DeleteDataToAPI();

            if (_configuration != null)
            {
                // Use configuration values
                string excelFilePath = _configuration["ExcelFilePath"];
                string supplierApiEndpoint = _configuration["SupplierApiEndpoint"];
                string intelexURL = _configuration["IntelexURLProd"];
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
                        //string? Inactive = null;

                        //while (worksheet.Cells[startRow, 1].Value != null)
                        //{
                        // Generate a GUID for each row
                        Guid rowGuid = Guid.NewGuid();
                        // Get data from Excel
                        string Type = "SupplierDelete";//worksheet.Cells[startRow, 1].Text;
                        string NewSupplierNumber = worksheet.Cells[startRow, 2].Text;
                        string OldSupplierNumber = worksheet.Cells[startRow, 3].Text;
                        string Name = worksheet.Cells[startRow, 4].Text;
                        string Inactive = worksheet.Cells[startRow, 5].Text;
                        // Add more columns as needed       

                        var suppApiEndpoint = intelexURL + supplierApiEndpoint; //endpoint for Supplier(Intelex)
                        string truncatedSupplierName = Name;//.Substring(0, Math.Min(5, Name.Length));  //truncated Name for Supplier(Intelex)

                        //// Query fields for each entity for Sublocation1
                        var supplierDataSet = await _dataExtractHelper.GetSupplierDataSet(suppApiEndpoint, Type, truncatedSupplierName, apiUsername, apiPassword, ctr, NewSupplierNumber);

                        /// Check if EHSIncidentMang_ContractorObjectSetResult is not null
                        if (supplierDataSet.EHSIncidentMang_ContractorObjectSetResult != null && supplierDataSet.EHSIncidentMang_ContractorObjectSetResult.value.Count > 0) // EXIST!! so UPDATE!!
                        {
                            Console.WriteLine($"Bulk Delete for Supplier in Intelex....\n");
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
                                    Console.WriteLine($"Supplier Id: {supplierId}");

                                    ////Payload Builder for Delete
                                    Console.WriteLine($"Deleting for Supplier MD: '{supp_ctr}' : '{supplierName}' to Intelex....");
                                    var response = deleteToApi.DeleteDataToApi($"{suppApiEndpoint}({supplierId})", apiUsername, apiPassword);
                                    supp_ctr++;
                                }

                            }
                            else
                            {
                                Console.WriteLine("No data found in EHSIncidentMang_ContractorObjectSetResult.value");
                            }
                        }
                        else
                        {
                            Console.WriteLine("EHSIncidentMang_ContractorObjectSetResult is null");


                            ////Payload Builder for Create/Post
                            //Console.WriteLine($"Syncing for Supplier MD: {ctr} : '{NewSupplierNumber}' : '{Name}' to Intelex....");
                            //var payloadCreate = SupplierPayloadBuilderHelper.BuildSupplierPayload_Create(rowGuid.ToString(), Inactive/*bool.TryParse(Inactive, out bool result)*/, Name, NewSupplierNumber);
                            //var jsonPayload = JsonConvert.SerializeObject(payloadCreate, Formatting.Indented);
                            //var response = postToApi.PostDataToApi(suppApiEndpoint, payloadCreate, apiUsername, apiPassword);

                        }
                        ctr++;
                        //    startRow++;
                        //}

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

