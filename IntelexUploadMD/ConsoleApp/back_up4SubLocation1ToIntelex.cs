//using IntelexUploadMD.Model.DataSets;
//using IntelexUploadMD.Services;
//using IntelexUploadMD.Services.ExtractHelper.Interface;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using OfficeOpenXml;
//using System.Net;
//using System.Net.Http.Headers;

//namespace IntelexUploadMD.ConsoleApp
//{
//    public class SubLocation1ToIntelex
//    {
//        private readonly ISubLocation1ExtractHelper _dataExtractHelper;
//        public static void SubLocation1ToIntelexFunc()
//        {
//            // Replace these with your actual Excel file path and API endpoint
//            //string excelFilePath = "C:\\Users\\regalam\\Documents\\AssetMD.xlsx";
//            //string apiEndpoint = "https://preprod-na.intelex.com/Login3/OdfjellTest/api/v2/object/EHSIncidentMang_csSubLocation1Object";
//            //string targetWorksheetName = "Asset"; // Replace with your actual worksheet name
//            //string apiUsername = "Marvin.Regala@odfjell.com";
//            //string apiPassword = "Odfjell2024!!";

//            // Load configuration from JSON file
//            var config = ConfigHelper.LoadConfig("local.settings.json");

//            if (config != null)
//            {
//                // Use configuration values
//                string excelFilePath = config.ExcelFilePath;
//                string subLocation1ApiEndpoint = config.SubLocation1ApiEndpoint;
//                string locationApiEndpoint = config.LocationApiEndpoint;
//                string intelexURL = config.IntelexURL;
//                string TargetWorksheetNameSubLocation1 = config.TargetWorksheetNameSubLocation1;
//                string apiUsername = config.ApiUsername;
//                string apiPassword = config.ApiPassword;

//                try
//                {
//                    var excelPackage = new ExcelPackage(new FileInfo(excelFilePath));

//                    //var sheetName = excelPackage.Workbook.Worksheets[0].Names; // checking sheet name

//                    // Find the target worksheet by name
//                    var worksheet = excelPackage.Workbook.Worksheets[TargetWorksheetNameSubLocation1];

//                    if (worksheet != null)//if (excelPackage.Workbook.Worksheets.Count > 0)
//                    {

//                        // Assuming your data starts from the second row (skip header)
//                        int startRow = 2;
//                        string? csInactive = null;

//                        while (worksheet.Cells[startRow, 1].Value != null)
//                        {

//                            // Generate a GUID for each row
//                            Guid rowGuid = Guid.NewGuid();
//                            // Get data from Excel
//                            string Type = worksheet.Cells[startRow, 1].Text;
//                            string Name = worksheet.Cells[startRow, 2].Text;
//                            string Parent = worksheet.Cells[startRow, 3].Text;
//                            // Add more columns as needed

//                            var subLoc1ApiEndpoint = intelexURL + subLocation1ApiEndpoint;
//                            var parentSubLoc1ApiEndpoint = intelexURL + locationApiEndpoint;

//                            ///WORKING important before
//                            ///Check in Intelex if data exists for Parent Location fields
//                            //var responseFromParentApi = GetDataFromParentApi(parentSubLoc1ApiEndpoint, Parent, apiUsername, apiPassword);

//                            //var content = responseFromParentApi.Content.ReadAsStringAsync().Result;
//                            //var jsonGetPayload = JsonConvert.SerializeObject(content, Formatting.Indented);


//                            var responseData = responseFromParentApi.Content.ReadAsStringAsync().Result;

//                            // Deserialize the JSON content into a C# object
//                            var responseObj = JsonConvert.DeserializeObject(responseData);

//                            // Serialize the object with indentation
//                            var jsonGetPayload = JsonConvert.SerializeObject(responseObj, Formatting.Indented);

//                            //try
//                            //{
//                            // Parse the JSON string
//                            JObject jsonObject = JObject.Parse(jsonGetPayload);

//                            // Access the value array
//                            JArray valueArray = (JArray)jsonObject["value"];

//                            // Access the first object in the array
//                            JObject firstObject = (JObject)valueArray[0];

//                            // Access the value of the "Id" key
//                            string idValue = (string)firstObject["Id"];

//                            //////string parentId = $"[{{\"Id\": \"{idValue}\"}}]";
//                            string parentId = $"[{{\"Id\": \"{idValue}\"}}]";
//                            ////string[] test = { parentId };
//                            ///


//                            // Query fields for each table
//                            var lineHandlingNominationDeleteDataSet = await _dataExtractHelper.GetLineHandlingNominationDeleteDataSet(id);

//                            // Post payload to honeywell passing in the create or update endpoint accordingly
//                            endpoint = _configuration["HoneywellTransactionalEndpoint"];
//                            var payload = lineHandlingNominationDeleteDataSet.BuildDeleteLineHandlingPayload(_configuration);
//                            var jsonPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);
//                            var response = await _honeywellService.PostAsync(endpoint, jsonPayload);






//                            //// Assume parentId is a List<string> containing IDs
//                            //List<string> parentIdList = new List<string>();

//                            //// Add IDs to parentIdList
//                            //parentIdList.Add(idValue);
//                            //// Add more IDs as needed

//                            //// Convert parentIdList to a JSON array string
//                            //string parentId = JsonConvert.SerializeObject(parentIdList);




//                            //[{"Id": "4529e6a6-ae16-4b8b-a823-b9b7da42715f"}]   
//                            //$"$filter=contains(csName,'{truncatedSearchString}')";

//                            // Now you can use the idValue variable containing the Id value
//                            //    Console.WriteLine("Id value: " + idValue);
//                            //}
//                            //catch (Exception ex)
//                            //{
//                            //    Console.WriteLine("Error: " + ex.Message);
//                            //}


//                            //Payload builder
//                            //var payload = new
//                            //{
//                            //    Id = rowGuid,
//                            //    Deleted = false,
//                            //    csInactive = csInactive,
//                            //    csName = Name,
//                            //    csMainLocation = parentId


//                            //    //csMainLocation = "[{ "Id": "4529e6a6-ae16-4b8b-a823-b9b7da42715f"}]"
//                            //    // Add more properties as needed
//                            //};






//                            // ////Parse parentId string to Guid
//                            //Guid parentIdGuid = Guid.Parse(parentId);

//                            // var payload = new SubLocation1
//                            // {
//                            //     Id = rowGuid.ToString(), // Assuming rowGuid is of type Guid
//                            //     Deleted = false,
//                            //     csInactive = csInactive,
//                            //     csName = Name,
//                            //     csMainLocation = new List<CsMainLocation>
//                            //     {
//                            //         new CsMainLocation { Id = parentIdGuid } // Assign parentIdGuid to Id property
//                            //     },
//                            //     csSubLocs2 = new List<CsSubLocs2>() // Initialize csSubLocs2 as needed
//                            // };

//                            //var jsonGetPayload = JsonConvert.SerializeObject(data, Formatting.Indented);

//                            ///Check in Intelex if data exists for SubLocation1 fields
//                            var responseFromChildApi = GetDataFromChildApi(subLoc1ApiEndpoint, Name, apiUsername, apiPassword);

//                            if (responseFromChildApi != null && responseFromChildApi.Content != null)
//                            {

//                                var responseData1 = responseFromChildApi.Content.ReadAsStringAsync().Result;

//                                // Deserialize the JSON content into a C# object
//                                var responseObj1 = JsonConvert.DeserializeObject<JObject>(responseData1);

//                                // Serialize the object with indentation
//                                var jsonGetPayload1 = JsonConvert.SerializeObject(responseObj1, Formatting.Indented);

//                                //if (responseFromChildApi.Content != null && responseFromChildApi.IsSuccessStatusCode)
//                                if (responseObj1["value"] != null && responseObj1["value"].Type == JTokenType.Array && !responseObj1["value"].Any())
//                                {

//                                    // Handle unsuccessful response
//                                    if (responseFromChildApi.Content != null)
//                                    {
//                                        //var errorContent = responseFromChildApi.Content.ReadAsStringAsync().Result;
//                                        //Console.WriteLine($"Failed to retrieve data from API. Status code: {responseFromChildApi.StatusCode}, Error: {errorContent}");
//                                        //Create via POST data to Intelex API since not existing
//                                        PostDataToApi(subLoc1ApiEndpoint, payload, apiUsername, apiPassword);
//                                    }
//                                    else
//                                    {
//                                        Console.WriteLine("Failed to retrieve data from API. Response was null.");
//                                        //Create via POST data to Intelex API since not existing
//                                        PostDataToApi(subLoc1ApiEndpoint, payload, apiUsername, apiPassword);
//                                    }



//                                }
//                                else
//                                {
//                                    // Process successful response
//                                    var content = responseFromChildApi.Content.ReadAsStringAsync().Result;
//                                    jsonGetPayload = JsonConvert.SerializeObject(content, Formatting.Indented);
//                                    Console.WriteLine("Data retrieved from API successfully.");

//                                }

//                            }
//                            else
//                            {
//                                // Handle the case where responseFromChildApi is null or content is null
//                                Console.WriteLine("No response or content from child API.");
//                            }

//                            startRow++;
//                        }

//                        Console.WriteLine("Data synchronization completed successfully.");
//                    }
//                    else
//                    {
//                        Console.WriteLine($"Worksheet '{TargetWorksheetNameSubLocation1}' not found in the Excel file.");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error: {ex.Message}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("Failed to load configuration from the JSON file.");
//            }
//        }

//        static async void PostDataToApi(string endPoint, object payload, string username, string password)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                /// Check payload via json
//                //var jsonGetPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

//                /// API expects json data
//                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload));
//                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


//                /// Convert the username and password to Base64 string for Basic Authentication
//                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

//                //string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

//                //// Append the filter query to the endpoint
//                //string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

//                // POST / CREATE to ILX API
//                var response = client.PostAsync(endPoint, content).Result;

//                if (response.IsSuccessStatusCode)
//                {
//                    Console.WriteLine($"Data posted to API successfully.");
//                }
//                else
//                {
//                    //Console.WriteLine($"Failed to post data to API. Status code: {response.StatusCode}");
//                    var responseBody = await response.Content.ReadAsStringAsync();
//                    Console.WriteLine($"Response body: {responseBody}");
//                }
//            }
//        }


//        private static HttpResponseMessage GetDataFromChildApi(string endPoint, string searchString, string username, string password)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                // Set up Basic Authentication
//                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

//                // Take only the first 5 characters from the left
//                string truncatedSearchString = searchString.Substring(0, Math.Min(5, searchString.Length));

//                // Build the filter query
//                string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

//                // Append the filter query to the endpoint
//                string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

//                // Perform GET request
//                var response = client.GetAsync(apiUrlWithFilter).Result;

//                //if (response.Content != null && response.IsSuccessStatusCode) //IsSuccessStatusCode
//                //{
//                //    // Process successful response
//                //    Console.WriteLine("Data retrieved from API successfully.");
//                //    return response.Content;
//                //}
//                //else
//                //{
//                //    // Handle unsuccessful response
//                //    Console.WriteLine($"Failed to retrieve data from API. Status code: {response.StatusCode}");
//                //    return response.Content;
//                //}

//                return response;
//            }
//        }

//        //WORKING
//        //private static HttpResponseMessage GetDataFromParentApi(string endPoint, string searchString, string username, string password)
//        //{
//        //    using (HttpClient client = new HttpClient())
//        //    {
//        //        // Set up Basic Authentication
//        //        string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
//        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

//        //        // Take only the first 5 characters from the left
//        //        //string truncatedSearchString = searchString.Substring(0, Math.Min(5, searchString.Length));

//        //        // Build the filter query
//        //        string filterQuery = $"$filter=contains(Path,'{searchString}')";

//        //        // Append the filter query to the endpoint
//        //        string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

//        //        // Perform GET request
//        //        var response = client.GetAsync(apiUrlWithFilter).Result;

//        //        return response;
//        //    }
//        //}

//        //private static string GetDataFromParentApi(string endPoint, string searchString, string username, string password)
//        //{
//        //    using (HttpClient client = new HttpClient())
//        //    {
//        //        // Set up Basic Authentication
//        //        string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
//        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

//        //        // Build the filter query
//        //        string filterQuery = $"$filter=contains(Path,'{searchString}')";

//        //        // Append the filter query to the endpoint
//        //        string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

//        //        // Perform GET request
//        //        var response = client.GetAsync(apiUrlWithFilter).Result;

//        //        if (response.IsSuccessStatusCode)
//        //        {
//        //            // Read the response content as string
//        //            string responseData = response.Content.ReadAsStringAsync().Result;
//        //            return responseData;
//        //        }
//        //        else
//        //        {
//        //            // Handle unsuccessful response
//        //            return null;
//        //        }
//        //    }
//        //}

//        //private static List<Location> GetDataFromParentApi(string endPoint, string searchString, string username, string password)
//        //{
//        //    List<Location> dataSet = new List<Location>();

//        //    using (HttpClient client = new HttpClient())
//        //    {
//        //        // Set up Basic Authentication
//        //        string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
//        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

//        //        // Build the filter query
//        //        string filterQuery = $"$filter=contains(Path,'{searchString}')";

//        //        // Append the filter query to the endpoint
//        //        string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

//        //        // Perform GET request
//        //        var response = client.GetAsync(apiUrlWithFilter).Result;

//        //        if (response.IsSuccessStatusCode)
//        //        {
//        //            // Read the response content as string
//        //            string responseData = response.Content.ReadAsStringAsync().Result;

//        //            // Deserialize JSON content into a list of YourDataModel objects
//        //            dataSet = JsonConvert.DeserializeObject<List<Location>>(responseData);
//        //        }
//        //        else
//        //        {
//        //            // Handle unsuccessful response
//        //            Console.WriteLine("Failed to retrieve data from API. Status code: " + response.StatusCode);
//        //        }
//        //    }

//        //    return dataSet;
//        //}


//    }
//}

