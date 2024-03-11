//using AzureKeyVault;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System.Text;
//using TmsSrvBus2Honeywell.Models.Response;
//using Client = SapClient.SapClient;

//namespace TmsSrvBus2Honeywell.Services
//{
//    public class IntelexService : IIntelexService
//    {
//        private readonly ILogger<SapService> _logger;
//        private readonly IConfiguration _configuration;
//        private Client _sapClient;

//        public SapService(
//            ILogger<SapService> logger,
//            IHttpClientFactory httpClientFactory,
//            IKeyVaultManager keyVaultManager,
//            IConfiguration configuration)
//        {
//            _logger = logger;
//            _configuration = configuration;
//            _sapClient = new Client(httpClientFactory.CreateClient("Sap"), keyVaultManager.GetSecret(configuration["SapODataKVSecretUserName"]).Result, keyVaultManager.GetSecret(configuration["SapODataKVSecretUserPassword"]).Result);
//        }

//        public async Task<List<T>> GetAsync<T>(string oDataFilter, string? selectFields = null, int? top = null, string? orderBy = null, bool? isFreeSpacePayloadSet = false, bool? isLongTextPayloadSet = false)
//        {
//            try
//            {
//                StringBuilder oDataEndpointBuilder = new StringBuilder();
//                oDataEndpointBuilder.Append(isFreeSpacePayloadSet.HasValue && isFreeSpacePayloadSet == true ? _configuration["SapODataDynamicDataServiceTankFreeSpaceUrl"] : _configuration["SapODataDynamicDataServiceUrl"]);
//                oDataEndpointBuilder.Append(typeof(T).Name);

//                if (isLongTextPayloadSet.HasValue && isLongTextPayloadSet.Value == true)
//                {
//                    oDataEndpointBuilder.Clear(); // Clear the previous content
//                    oDataEndpointBuilder.Append(_configuration["SapODataDynamicDataServiceLongTextUrl"]);
//                    oDataEndpointBuilder.Append(typeof(T).Name);

//                    oDataEndpointBuilder.Append("?$expand=LongTextSet&");
//                }
//                else
//                {
//                    oDataEndpointBuilder.Append("?");
//                }

//                if (!string.IsNullOrEmpty(selectFields))
//                {
//                    oDataEndpointBuilder.Append("$format=json&$select=");
//                    oDataEndpointBuilder.Append(selectFields);
//                }
//                else
//                {
//                    oDataEndpointBuilder.Append("$format=json");
//                }

//                if (!string.IsNullOrEmpty(orderBy))
//                {
//                    oDataEndpointBuilder.Append("&$orderby=");
//                    oDataEndpointBuilder.Append(orderBy);
//                }

//                if (top.HasValue && top.Value > 0)
//                {
//                    oDataEndpointBuilder.Append("&$top=");
//                    oDataEndpointBuilder.Append(top.Value);
//                }

//                if (!string.IsNullOrEmpty(oDataFilter))
//                {
//                    oDataEndpointBuilder.Append("&$filter=");
//                }

//                _logger.LogInformation($"Fetching EntitySet Data! Url={oDataEndpointBuilder}{oDataFilter}");
//                var response = await _sapClient.Get(oDataEndpointBuilder.ToString(), null, oDataFilter);

//                if (response.Status == System.Net.HttpStatusCode.OK)
//                {
//                    var entitySetApiResponse = JsonConvert.DeserializeObject<EntitySetApiResponse<T>>(response.Content);
//                    var modifiedObjectIds = entitySetApiResponse.d.results;
//                    return modifiedObjectIds;
//                }
//                else
//                {
//                    throw new Exception(response.Content);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"An error occurred while fetching {typeof(T).Name} data! Module=SapService.GetAsync, Error={ex.Message}");
//                throw;
//            }
//        }
//    }
//}
