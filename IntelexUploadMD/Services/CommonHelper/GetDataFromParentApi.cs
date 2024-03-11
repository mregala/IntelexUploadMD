using IntelexUploadMD.Model.DataSets;
using IntelexUploadMD.Services.ExtractHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntelexUploadMD.Services.ExtractHelper.DataExtractHelper
{
    public class GetDataFromParentApi  //LineHandlingNominationDeleteExtractHelper : ILineHandlingNominationDeleteExtractHelper
    {
        public static HttpResponseMessage GetDataFromParentApiFunc(string endPoint, string searchString, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up Basic Authentication
                string authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                // Take only the first 5 characters from the left
                //string truncatedSearchString = searchString.Substring(0, Math.Min(5, searchString.Length));

                // Build the filter query
                string filterQuery = $"$filter=contains(Path,'{searchString}')";

                // Append the filter query to the endpoint
                string apiUrlWithFilter = $"{endPoint}?{filterQuery}";

                // Perform GET request
                var response = client.GetAsync(apiUrlWithFilter).Result;

                return response;
            }
        }


    }
}
