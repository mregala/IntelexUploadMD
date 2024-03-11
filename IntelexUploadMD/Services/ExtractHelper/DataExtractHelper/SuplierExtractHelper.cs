using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Model.EntitySets;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace IntelexUploadMD.Services.ExtractHelper.DataExtractHelper
{
    public class SupplierExtractHelper : ISupplierExtractHelper
    {
        public async Task<SupplierDataSets> GetSupplierDataSet(string endPoint, string type, string searchString, string username, string password, int ctr, string contractorNo)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Define filters and select fields based on the type
                string filter = "";
                string select = "";
                string dummySearchString = "dummy";
                object entitySetResult = null;
                //var ctr = 0;

                //switch (type)
                //{
                //    case "SupplierDelete":
                //        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                //        break;
                //    case "Supplier":
                //        if (!string.IsNullOrEmpty(searchString))
                //        {
                //            searchString = HttpUtility.UrlEncode(searchString);
                //            filter = $"$filter=contains(Name,'{searchString}') or ContractorNo eq '{contractorNo}'";
                //        }
                //        else
                //        {
                //            filter = $"$filter=contains(Name,'{HttpUtility.UrlEncode(dummySearchString)}')";
                //        }
                //        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                //        break;
                //    default:
                //        if (!string.IsNullOrEmpty(searchString))
                //        {
                //            searchString = HttpUtility.UrlEncode(searchString);
                //            filter = $"$filter=contains(Name,'{searchString}') and ContractorNo eq '{contractorNo}'";
                //        }
                //        else
                //        {
                //            filter = $"$filter=contains(Name,'{HttpUtility.UrlEncode(dummySearchString)}')";
                //        }
                //        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                //        break;
                //}
                switch (type)
                {
                    case "SupplierDelete":
                        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                        break;
                    case "Supplier":
                        if (!string.IsNullOrEmpty(searchString))
                        {
                            searchString = HttpUtility.UrlEncode(searchString.Replace("'", "''"));
                            filter = $"$filter=contains(Name,'{searchString}') or ContractorNo eq '{contractorNo}'";
                        }
                        else
                        {
                            filter = $"$filter=contains(Name,'{HttpUtility.UrlEncode(dummySearchString.Replace("'", "''"))}')";
                        }
                        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                        break;
                    default:
                        if (!string.IsNullOrEmpty(searchString))
                        {
                            searchString = HttpUtility.UrlEncode(searchString.Replace("'", "''"));
                            filter = $"$filter=contains(Name,'{searchString}') and ContractorNo eq '{contractorNo}'";
                        }
                        else
                        {
                            filter = $"$filter=contains(Name,'{HttpUtility.UrlEncode(dummySearchString.Replace("'", "''"))}')";
                        }
                        select = $"$select=Id,Inactive,Name,RecordNo,ContractorNo";
                        break;
                }


                // Construct URL for the dataset based on type or default
                string url = $"{endPoint}?{select}&{filter}";

                try
                {
                    // Log before sending the HTTP GET request
                    Console.WriteLine($"Sending HTTP GET request for Row: '{ctr}' Supplier: '{searchString}'...");

                    // Send HTTP GET request for the dataset
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Log after the HTTP GET request is sent
                    Console.WriteLine($"HTTP GET request sent for Row: '{ctr}' Supplier: '{searchString}'...");

                    // Ensure successful response
                    response.EnsureSuccessStatusCode();

                    // Read response body
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response into entity set based on the type
                    switch (type)
                    {
                        case "Supplier":
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_ContractorObjectSet>(responseBody);
                            break;
                        default:
                            // Deserialize SysLocationEntitySet for the default case
                            entitySetResult = JsonSerializer.Deserialize<EHSIncidentMang_ContractorObjectSet>(responseBody);
                            break;
                    }

                    // Create SupplierDataSets object and populate with retrieved data
                    var supplierDataSet = new SupplierDataSets();

                    // Assign the entity set result based on type or default
                    switch (type)
                    {
                        case "Supplier":
                            supplierDataSet.EHSIncidentMang_ContractorObjectSetResult = (EHSIncidentMang_ContractorObjectSet)entitySetResult;
                            break;
                        default:
                            // Assign the entity set result to SupplierEntitySetResult for the default case
                            supplierDataSet.EHSIncidentMang_ContractorObjectSetResult = (EHSIncidentMang_ContractorObjectSet)entitySetResult;
                            break;
                    }

                    return supplierDataSet;
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request exception
                    Console.WriteLine($"HTTP GET request failed: {ex.Message}");
                    throw;
                }
                catch (TaskCanceledException ex)
                {
                    // Handle task cancellation due to timeout
                    Console.WriteLine($"HTTP GET request timed out: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
