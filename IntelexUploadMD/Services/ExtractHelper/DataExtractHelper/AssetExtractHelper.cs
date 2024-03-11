using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.EntitySets;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelexUploadMD.Services.ExtractHelper.DataExtractHelper
{
    public class AssetExtractHelper : IAssetExtractHelper
    {
        public async Task<AssetDataSets> GetAssetDataSet(string endPoint, string type, string searchString, string parent, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Define filters and select fields based on the type
                string filter = "";
                string select = "";
                object entitySetResult = null;

                switch (type)
                {
                    case "Sublocation1":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,csMainLocation,csSubLocs2";
                        break;
                    case "Sublocation2":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,SubLocs3";
                        break;
                    case "Sublocation3":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive";
                        break;
                    default:
                        // For the default case, retrieve SysLocationEntitySet
                        filter = $"$filter=contains(Path,'{searchString}')";
                        select = $"$select=Id,LocationCode,Name,Path";
                        break;
                }

                // Construct URL for the dataset based on type or default
                string url = $"{endPoint}?{select}&{filter}";

                try
                {
                    // Log before sending the HTTP GET request
                    Console.WriteLine("Sending HTTP GET request...");

                    // Send HTTP GET request for the dataset
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Log after the HTTP GET request is sent
                    Console.WriteLine("HTTP GET request sent.");

                    // Ensure successful response
                    response.EnsureSuccessStatusCode();

                    // Read response body
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response into entity set based on the type
                    switch (type)
                    {
                        case "Sublocation1":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation1ObjectSet>(responseBody);
                            break;
                        case "Sublocation2":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation2ObjectSet>(responseBody);
                            break;
                        case "Sublocation3":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation3ObjectSet>(responseBody);
                            break;
                        default:
                            // Deserialize SysLocationEntitySet for the default case
                            entitySetResult = JsonSerializer.Deserialize<SysLocationEntitySet>(responseBody);
                            break;
                    }

                    // Create AssetDataSets object and populate with retrieved data
                    var assetDataSet = new AssetDataSets();

                    // Assign the entity set result based on type or default
                    switch (type)
                    {
                        case "Sublocation1":
                            assetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult = (EHSIncidentMang_csSubLocation1ObjectSet)entitySetResult;
                            break;
                        case "Sublocation2":
                            assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult = (EHSIncidentMang_csSubLocation2ObjectSet)entitySetResult;
                            break;
                        case "Sublocation3":
                            assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult = (EHSIncidentMang_csSubLocation3ObjectSet)entitySetResult;
                            break;
                        default:
                            // Assign the entity set result to SysLocationEntitySetResult for the default case
                            assetDataSet.SysLocationEntitySetResult = (SysLocationEntitySet)entitySetResult;
                            break;
                    }

                    return assetDataSet;
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exception
                    Console.WriteLine($"HTTP request failed: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
                catch (TaskCanceledException ex)
                {
                    // Handle task cancellation due to timeout
                    Console.WriteLine($"HTTP request timed out: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
            }
        }


        public async Task<AssetDataSets> GetParentAssetDataSet(string endPoint, string type, string searchString, string parent, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Define filters and select fields based on the type
                string filter = "";
                string select = "";
                object entitySetResult = null;

                switch (type)
                {
                    case "Sublocation2":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,csMainLocation,csSubLocs2";
                        break;
                    case "Sublocation3":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,SubLocs3";
                        break;
                    //case "Sublocation3":
                    //    filter = $"$filter=contains(csName,'{searchString}')";
                    //    select = $"$select=Id,csName,csInactive";
                    //    break;
                    default:
                        // For the default case, retrieve SysLocationEntitySet
                        filter = $"$filter=contains(Path,'{searchString}')";
                        select = $"$select=Id,LocationCode,Name,Path";
                        break;
                }

                // Construct URL for the dataset based on type or default
                string url = $"{endPoint}?{select}&{filter}";

                try
                {
                    // Log before sending the HTTP GET request
                    Console.WriteLine("Sending HTTP GET request...");

                    // Send HTTP GET request for the dataset
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Log after the HTTP GET request is sent
                    Console.WriteLine("HTTP GET request sent.");

                    // Ensure successful response
                    response.EnsureSuccessStatusCode();

                    // Read response body
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response into entity set based on the type
                    switch (type)
                    {
                        case "Sublocation2":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation1ObjectSet>(responseBody);
                            break;
                        case "Sublocation3":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation2ObjectSet>(responseBody);
                            break;
                        //case "Sublocation3":
                        //    entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation3ObjectSet>(responseBody);
                        //    break;
                        default:
                            // Deserialize SysLocationEntitySet for the default case
                            entitySetResult = JsonSerializer.Deserialize<SysLocationEntitySet>(responseBody);
                            break;
                    }

                    // Create AssetDataSets object and populate with retrieved data
                    var assetParentDataSet = new AssetDataSets();

                    // Assign the entity set result based on type or default
                    switch (type)
                    {
                        //case "Sublocation1":
                        //    assetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult = (EHSIncidentMang_csSubLocation1ObjectSet)entitySetResult;
                        //    break;
                        case "Sublocation2":
                            assetParentDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult = (EHSIncidentMang_csSubLocation1ObjectSet)entitySetResult;
                            break;
                        case "Sublocation3":
                            assetParentDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult = (EHSIncidentMang_csSubLocation2ObjectSet)entitySetResult;
                            break;
                        default:
                            // Assign the entity set result to SysLocationEntitySetResult for the default case
                            assetParentDataSet.SysLocationEntitySetResult = (SysLocationEntitySet)entitySetResult;
                            break;
                    }

                    return assetParentDataSet;
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exception
                    Console.WriteLine($"HTTP request failed: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
                catch (TaskCanceledException ex)
                {
                    // Handle task cancellation due to timeout
                    Console.WriteLine($"HTTP request timed out: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
            }
        }

        public async Task<AssetDataSets> GetChildAssetDataSet(string endPoint, string type, string searchString, string parent, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Define filters and select fields based on the type
                string filter = "";
                string select = "";
                object entitySetResult = null;

                switch (type)
                {
                    case "Sublocation1":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,SubLocs3";
                        break;
                    case "Sublocation2":
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive";
                        break;
                    default:
                        // For the default case, retrieve SysLocationEntitySet Child : EHSIncidentMang_csSubLocation1ObjectSet
                        filter = $"$filter=contains(csName,'{searchString}')";
                        select = $"$select=Id,csName,csInactive,csMainLocation,csSubLocs2";
                        break;
                }

                // Construct URL for the dataset based on type or default
                string url = $"{endPoint}?{select}&{filter}";

                try
                {
                    // Log before sending the HTTP GET request
                    Console.WriteLine("Sending HTTP GET request...");

                    // Send HTTP GET request for the dataset
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Log after the HTTP GET request is sent
                    Console.WriteLine("HTTP GET request sent.");

                    // Ensure successful response
                    response.EnsureSuccessStatusCode();

                    // Read response body
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response into entity set based on the type
                    switch (type)
                    {
                        case "Sublocation1":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation2ObjectSet>(responseBody);
                            break;
                        case "Sublocation2":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation3ObjectSet>(responseBody);
                            break;
                        default:
                            // Deserialize SysLocationEntitySet for the default case
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_csSubLocation1ObjectSet>(responseBody);
                            break;
                    }

                    // Create AssetDataSets object and populate with retrieved data
                    var assetDataSet = new AssetDataSets();

                    // Assign the entity set result based on type or default
                    switch (type)
                    {
                        case "Sublocation1":
                            assetDataSet.EHSIncidentMang_csSubLocation2ObjectSetResult = (EHSIncidentMang_csSubLocation2ObjectSet)entitySetResult;
                            break;
                        case "Sublocation2":
                            assetDataSet.EHSIncidentMang_csSubLocation3ObjectSetResult = (EHSIncidentMang_csSubLocation3ObjectSet)entitySetResult;
                            break;
                        default:
                            // Assign the entity set result to SysLocationEntitySetResult for the default case
                            assetDataSet.EHSIncidentMang_csSubLocation1ObjectSetResult = (EHSIncidentMang_csSubLocation1ObjectSet)entitySetResult;
                            break;
                    }

                    return assetDataSet;
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exception
                    Console.WriteLine($"HTTP request failed: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
                catch (TaskCanceledException ex)
                {
                    // Handle task cancellation due to timeout
                    Console.WriteLine($"HTTP request timed out: {ex.Message}");
                    // You can throw the exception or handle it gracefully based on your requirements.
                    throw;
                }
            }
        }


    }
}
