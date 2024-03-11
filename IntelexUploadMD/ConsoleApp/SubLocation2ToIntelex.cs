using IntelexUploadMD.Services;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using IntelexUploadMD.Services.PayloadBuilder;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;

namespace IntelexUploadMD.ConsoleApp
{
    public class SubLocation2ToIntelex
    {
        private readonly IAssetExtractHelper _dataExtractHelper;
        private string? subLocation1Id;
        private string? subLocation3Id;

        public SubLocation2ToIntelex(
            IAssetExtractHelper dataExtractHelper)
        {
            _dataExtractHelper = dataExtractHelper;
        }

        public async Task SubLocation2ToIntelexFunc(/*ISubLocation1ExtractHelper _dataExtractHelper*/)
        {
            // Load configuration from JSON file
            var config = ConfigHelper.LoadConfig("local.settings.json");

            if (config != null)
            {
                // Use configuration values
                string excelFilePath = config.ExcelFilePath;
                string locationApiEndpoint = config.LocationApiEndpoint;
                string subLocation1ApiEndpoint = config.SubLocation1ApiEndpoint;
                string subLocation2ApiEndpoint = config.SubLocation2ApiEndpoint;
                string subLocation3ApiEndpoint = config.SubLocation3ApiEndpoint;
                string intelexURL = config.IntelexURLProd;
                string TargetWorksheetNameSubLocation2 = config.TargetWorksheetNameSubLocation2;
                string apiUsername = config.ApiUsername;
                string apiPassword = config.ApiPassword;

                try
                {
                    var excelPackage = new ExcelPackage(new FileInfo(excelFilePath));

                    // Find the target worksheet by name
                    var worksheet = excelPackage.Workbook.Worksheets[TargetWorksheetNameSubLocation2];

                    if (worksheet != null)//if (excelPackage.Workbook.Worksheets.Count > 0)
                    {

                        // Assuming your data starts from the second row (skip header)
                        int startRow = 2;
                        string? csInactive = null;

                        while (worksheet.Cells[startRow, 1].Value != null)
                        {

                            // Generate a GUID for each row
                            Guid rowGuid = Guid.NewGuid();
                            // Get data from Excel
                            string Type = worksheet.Cells[startRow, 1].Text;
                            string Name = worksheet.Cells[startRow, 2].Text;
                            string Parent = worksheet.Cells[startRow, 3].Text;
                            // Add more columns as needed       


                            var subLoc2ApiEndpoint = intelexURL + subLocation2ApiEndpoint; //endpoint for SubLocation2
                            //var subLoc1UpdateApiEndpoint = intelexURL + subLocation1ApiEndpoint; //endpoint for SubLocation1 Update
                            var parentSubLoc2ApiEndpoint = intelexURL + subLocation1ApiEndpoint; //parent endpoint for SubLocation1
                            var childSubLoc2ApiEndpoint = intelexURL + subLocation3ApiEndpoint; //parent endpoint for SubLocation3

                            string truncatedAssetName = Name.Substring(0, Math.Min(9, Name.Length));
                            //string truncatedAssetName = Name;

                            //// Query fields for each entity for Sublocation2
                            var assetDataSet = await _dataExtractHelper.GetAssetDataSet(subLoc2ApiEndpoint, Type, truncatedAssetName, Parent, apiUsername, apiPassword);

                            //// Check PARENT Id of SubLocation1 - SubLocation1
                            var parentAssetDataSet = await _dataExtractHelper.GetParentAssetDataSet(parentSubLoc2ApiEndpoint, Type, Parent, Parent, apiUsername, apiPassword);

                            //// Check CHILD Id of SubLocation1 - Sublocation3
                            var childAssetDataSet = await _dataExtractHelper.GetChildAssetDataSet(childSubLoc2ApiEndpoint, Type, truncatedAssetName, Parent, apiUsername, apiPassword);


                            /// PARENT!!!!
                            ////Get Parent ID for EHSIncidentMang_csSubLocation1ObjectSet - SysLocationEntitySet
                            if (parentAssetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult != null && parentAssetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value.Count > 0) //EXISTS parent!!
                            {
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var subLocation1Values = parentAssetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult.value;

                                // Check if subLocation1Values is not null and contains any elements
                                if (subLocation1Values != null && subLocation1Values.Any())
                                {
                                    // Loop through each item in subLocation1Values
                                    foreach (var item in subLocation1Values)
                                    {
                                        subLocation1Id = item.Id;
                                        Console.WriteLine($"Location Id: {subLocation1Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No data found in EHSIncidentMang_csSubLocation1ObjectSetResult.value");
                                }
                            }
                            else
                            {
                                Console.WriteLine("EHSIncidentMang_csSubLocation1ObjectSetResult is null");
                            }


                            /// CHILD!!!
                            if (childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult != null && childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.Count > 0) //EXISTS parent!!
                            {
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var subLocation3Values = childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value;

                                // Check if subLocation1Values is not null and contains any elements
                                if (subLocation3Values != null && subLocation3Values.Any())
                                {
                                    // Loop through each item in subLocation1Values
                                    foreach (var item in subLocation3Values)
                                    {
                                        subLocation3Id = item.Id;
                                        Console.WriteLine($"SubLocation3 Id: {subLocation3Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No data found in EHSIncidentMang_csSubLocation3ObjectSetResult.value");
                                }
                            }
                            else
                            {
                                Console.WriteLine("EHSIncidentMang_csSubLocation3ObjectSetResult is null");
                            }

                            /// Current level
                            //// Check if EHSIncidentMang_csSubLocation1ObjectSetResult is not null
                            if (assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult != null && assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.Count > 0) // EXIST!! so UPDATE!!
                            {
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var subLocation2Values = assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value;

                                // Check if subLocation1Values is not null and contains any elements
                                if (subLocation2Values != null && subLocation2Values.Any())
                                {
                                    //// Loop through each item in subLocation1Values
                                    //foreach (var item in subLocation1Values)
                                    //{
                                    //    // Access the Id property of each item
                                    //    var subLocation1Id = item.Id;
                                    //    var subLocation1csName = item.csName;
                                    //    var subLocation1csInactive = item.csInactive;

                                    //    // Do something with subLocationId
                                    //    Console.WriteLine($"SubLocation Id: {subLocation1Id}");
                                    //}

                                    ////Payload Builder for Update/Patch
                                    var payloadUpdate = assetDataSet.BuildSubLocation2Payload_Update(subLocation1Id, Name);
                                    var jsonPayload = JsonConvert.SerializeObject(payloadUpdate, Formatting.Indented);
                                    var response = PatchDataToApi($"{subLoc2ApiEndpoint}({subLocation2Values.FirstOrDefault().Id})", payloadUpdate, apiUsername, apiPassword);

                                    //Updating PARENT!!! for SubLocation1
                                    var payloadUpdateParent = parentAssetDataSet.BuildSubLocation1Payload_UpdateChild(assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.FirstOrDefault().Id);
                                    jsonPayload = JsonConvert.SerializeObject(payloadUpdateParent, Formatting.Indented);
                                    response = PatchDataToApi($"{parentSubLoc2ApiEndpoint}({subLocation1Id})", payloadUpdateParent, apiUsername, apiPassword);

                                }
                                else
                                {
                                    Console.WriteLine("No data found in EHSIncidentMang_csSubLocation2ObjectSetResult.value");
                                }
                            }
                            else
                            {
                                Console.WriteLine("EHSIncidentMang_csSubLocation2ObjectSetResult is null");


                                ////Payload Builder for Create/Post
                                var payloadCreate = SubLocation2PayloadBuilderHelper.BuildSubLocation2Payload_Create(rowGuid.ToString(), Name, csInactive, subLocation1Id);
                                var jsonPayload = JsonConvert.SerializeObject(payloadCreate, Formatting.Indented);
                                var response = PostDataToApi(subLoc2ApiEndpoint, payloadCreate, apiUsername, apiPassword);

                                //Updating PARENT!!! for SubLocation1
                                var payloadUpdateParent = parentAssetDataSet.BuildSubLocation1Payload_UpdateChild(rowGuid.ToString());
                                jsonPayload = JsonConvert.SerializeObject(payloadUpdateParent, Formatting.Indented);
                                response = PatchDataToApi($"{parentSubLoc2ApiEndpoint}({subLocation1Id})", payloadUpdateParent, apiUsername, apiPassword);

                                //var payloadUpdateParent = assetDataSet.BuildSubLocation1Payload_Update(subLocation1Id);
                                //jsonPayload = JsonConvert.SerializeObject(payloadUpdateParent, Formatting.Indented);
                                //response = PatchDataToApi($"{subLoc2ApiEndpoint}({subLocation1Values.FirstOrDefault().Id})", payloadUpdateParent, apiUsername, apiPassword);
                            }

                            startRow++;
                        }

                        Console.WriteLine("Data synchronization completed successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Worksheet '{TargetWorksheetNameSubLocation2}' not found in the Excel file.");
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

        public async Task PostDataToApi(string endPoint, object payload, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                /// Check payload via json
                //var jsonGetPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

                /// API expects json data
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                /// Convert the username and password to Base64 string for Basic Authentication
                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                //string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

                //// Append the filter query to the endpoint
                //string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

                // POST / CREATE to ILX API
                var response = client.PostAsync(endPoint, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Data posted to API successfully.");
                }
                else
                {
                    //Console.WriteLine($"Failed to post data to API. Status code: {response.StatusCode}");
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response body: {responseBody}");
                }
            }
        }

        public async Task PatchDataToApi(string endPoint, object payload, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                /// Check payload via json
                var jsonGetPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

                /// API expects json data
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                /// Convert the username and password to Base64 string for Basic Authentication
                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                //string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

                //// Append the filter query to the endpoint
                //string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

                // POST / CREATE to ILX API
                var response = client.PatchAsync(endPoint, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Data patched to API successfully.");
                }
                else
                {
                    //Console.WriteLine($"Failed to post data to API. Status code: {response.StatusCode}");
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response body: {responseBody}");
                }
            }
        }


        private static HttpResponseMessage GetDataFromChildApi(string endPoint, string searchString, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Take only the first 5 characters from the left
                string truncatedSearchString = searchString.Substring(0, Math.Min(5, searchString.Length));

                // Build the filter query
                string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

                // Append the filter query to the endpoint
                string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

                // Perform GET request
                var response = client.GetAsync(apiUrlWithFilter).Result;

                //if (response.Content != null && response.IsSuccessStatusCode) //IsSuccessStatusCode
                //{
                //    // Process successful response
                //    Console.WriteLine("Data retrieved from API successfully.");
                //    return response.Content;
                //}
                //else
                //{
                //    // Handle unsuccessful response
                //    Console.WriteLine($"Failed to retrieve data from API. Status code: {response.StatusCode}");
                //    return response.Content;
                //}

                return response;
            }
        }


        //WORKING
        //private static HttpResponseMessage GetDataFromParentApi(string endPoint, string searchString, string username, string password)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Set up Basic Authentication
        //        string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

        //        // Take only the first 5 characters from the left
        //        //string truncatedSearchString = searchString.Substring(0, Math.Min(5, searchString.Length));

        //        // Build the filter query
        //        string filterQuery = $"$filter=contains(Path,'{searchString}')";

        //        // Append the filter query to the endpoint
        //        string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

        //        // Perform GET request
        //        var response = client.GetAsync(apiUrlWithFilter).Result;

        //        return response;
        //    }
        //}

    }
}

