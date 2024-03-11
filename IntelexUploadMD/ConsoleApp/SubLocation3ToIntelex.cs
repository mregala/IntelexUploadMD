using IntelexUploadMD.Services;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using IntelexUploadMD.Services.PayloadBuilder;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;

namespace IntelexUploadMD.ConsoleApp
{
    public class SubLocation3ToIntelex
    {
        private readonly IAssetExtractHelper _dataExtractHelper;
        private string? subLocation2Id;
        private string? subLocation3Id;

        public SubLocation3ToIntelex(
            IAssetExtractHelper dataExtractHelper)
        {
            _dataExtractHelper = dataExtractHelper;
        }

        public async Task SubLocation3ToIntelexFunc(/*ISubLocation1ExtractHelper _dataExtractHelper*/)
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
                string TargetWorksheetNameSubLocation3 = config.TargetWorksheetNameSubLocation3;
                string apiUsername = config.ApiUsername;
                string apiPassword = config.ApiPassword;

                try
                {
                    var excelPackage = new ExcelPackage(new FileInfo(excelFilePath));

                    // Find the target worksheet by name
                    var worksheet = excelPackage.Workbook.Worksheets[TargetWorksheetNameSubLocation3];

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
                            string Old_Name = worksheet.Cells[startRow, 4].Text;
                            // Add more columns as needed       


                            var subLoc3ApiEndpoint = intelexURL + subLocation3ApiEndpoint; //endpoint for SubLocation3
                            //var subLoc1UpdateApiEndpoint = intelexURL + subLocation1ApiEndpoint; //endpoint for SubLocation1 Update
                            var parentSubLoc3ApiEndpoint = intelexURL + subLocation2ApiEndpoint; //parent endpoint for SubLocation1
                            var childSubLoc3ApiEndpoint = intelexURL + subLocation3ApiEndpoint; //parent endpoint for SubLocation3

                            string truncatedOldAssetName = Old_Name;//Name.Substring(0, Math.Min(20, Name.Length));
                            string truncatedAssetName = Name;//Name.Substring(0, Math.Min(20, Name.Length));

                            //// Query fields for each entity for Sublocation3
                            var assetDataSet = await _dataExtractHelper.GetAssetDataSet(subLoc3ApiEndpoint, Type, truncatedOldAssetName, Parent, apiUsername, apiPassword);

                            //// Check PARENT Id of SubLocation1 - SubLocation2
                            var parentAssetDataSet = await _dataExtractHelper.GetParentAssetDataSet(parentSubLoc3ApiEndpoint, Type, Parent, Parent, apiUsername, apiPassword);

                            //// Check CHILD Id of SubLocation1 - Sublocation3
                            //var childAssetDataSet = await _dataExtractHelper.GetChildAssetDataSet(childSubLoc3ApiEndpoint, Type, truncatedOldAssetName, Parent, apiUsername, apiPassword);


                            /// PARENT!!!!
                            ////Get Parent ID for EHSIncidentMang_csSubLocation1ObjectSet - SysLocationEntitySet
                            if (parentAssetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult != null && parentAssetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value.Count > 0) //EXISTS parent!!
                            {
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var subLocation2Values = parentAssetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult.value;

                                // Check if subLocation1Values is not null and contains any elements
                                if (subLocation2Values != null && subLocation2Values.Any())
                                {
                                    // Loop through each item in subLocation1Values
                                    foreach (var item in subLocation2Values)
                                    {
                                        subLocation2Id = item.Id;
                                        Console.WriteLine($"Location Id: {subLocation2Id}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No data found in EHSIncidentMang_csSubLocation2ObjectSetResult.value");
                                }
                            }
                            else
                            {
                                Console.WriteLine("EHSIncidentMang_csSubLocation2ObjectSetResult is null");
                            }


                            ///// CHILD!!!
                            //if (childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult != null && childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.Count > 0) //EXISTS parent!!
                            //{
                            //    // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                            //    var subLocation3Values = childAssetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value;

                            //    // Check if subLocation1Values is not null and contains any elements
                            //    if (subLocation3Values != null && subLocation3Values.Any())
                            //    {
                            //        // Loop through each item in subLocation1Values
                            //        foreach (var item in subLocation3Values)
                            //        {
                            //            subLocation3Id = item.Id;
                            //            Console.WriteLine($"SubLocation3 Id: {subLocation3Id}");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine("No data found in EHSIncidentMang_csSubLocation3ObjectSetResult.value");
                            //    }
                            //}
                            //else
                            //{
                            //    Console.WriteLine("EHSIncidentMang_csSubLocation3ObjectSetResult is null");
                            //}

                            /// Current level
                            //// Check if EHSIncidentMang_csSubLocation1ObjectSetResult is not null
                            if (assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult != null && assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.Count > 0) // EXIST!! so UPDATE!!
                            {
                                // Access the list of values EHSIncidentMang_csSubLocation1ObjectSetValue
                                var subLocation3Values = assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value;

                                // Check if subLocation1Values is not null and contains any elements
                                if (subLocation3Values != null && subLocation3Values.Any())
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
                                    var payloadUpdate = assetDataSet.BuildSubLocation3Payload_Update(subLocation2Id,Name);
                                    var jsonPayload = JsonConvert.SerializeObject(payloadUpdate, Formatting.Indented);
                                    var response = PatchDataToApi($"{subLoc3ApiEndpoint}({subLocation3Values.FirstOrDefault().Id})", payloadUpdate, apiUsername, apiPassword);

                                    //Updating PARENT!!! for SubLocation2
                                    var payloadUpdateParent = parentAssetDataSet.BuildSubLocation2Payload_UpdateParent(assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult.value.FirstOrDefault().Id);
                                    jsonPayload = JsonConvert.SerializeObject(payloadUpdateParent, Formatting.Indented);
                                    response = PatchDataToApi($"{parentSubLoc3ApiEndpoint}({subLocation2Id})", payloadUpdateParent, apiUsername, apiPassword);

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
                                var payloadCreate = SubLocation2PayloadBuilderHelper.BuildSubLocation2Payload_Create(rowGuid.ToString(), Name, csInactive, subLocation2Id);
                                var jsonPayload = JsonConvert.SerializeObject(payloadCreate, Formatting.Indented);
                                var response = PostDataToApi(subLoc3ApiEndpoint, payloadCreate, apiUsername, apiPassword);

                                //Updating PARENT!!! for SubLocation2
                                var payloadUpdateParent = parentAssetDataSet.BuildSubLocation2Payload_UpdateParent(rowGuid.ToString());
                                jsonPayload = JsonConvert.SerializeObject(payloadUpdateParent, Formatting.Indented);
                                response = PatchDataToApi($"{parentSubLoc3ApiEndpoint}({subLocation2Id})", payloadUpdateParent, apiUsername, apiPassword);

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
                        Console.WriteLine($"Worksheet '{TargetWorksheetNameSubLocation3}' not found in the Excel file.");
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

