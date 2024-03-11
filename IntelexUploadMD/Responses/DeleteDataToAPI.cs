using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntelexUploadMD.Responses
{
    public class DeleteDataToAPI
    {
        public async Task DeleteDataToApi(string endPoint, /*object payload,*/ string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                ///// Check payload via json
                //var jsonGetPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

                ///// API expects json data
                //var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload));
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                /// Convert the username and password to Base64 string for Basic Authentication
                string authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                //string filterQuery = $"$filter=contains(csName,'{truncatedSearchString}')";

                //// Append the filter query to the endpoint
                //string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

                // DELETE to ILX API
                var response = client.DeleteAsync(endPoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Data deleted to API successfully.\n");
                }
                else
                {
                    //Console.WriteLine($"Failed to post data to API. Status code: {response.StatusCode}");
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response body: {responseBody}\n");
                }
            }
        }

    }
}
