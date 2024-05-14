using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Services.Interface;
using System.Text;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class BaseService : IBaseService
    {
        public APIResponse _responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        public APIResponse responseModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BaseService(IHttpClientFactory httpClient)
        {

            _responseModel = new APIResponse();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("RPRENTAL_API");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                HttpResponseMessage apiResponse = null!;
                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<T>(apiContent);


                return apiResult!;

            }
            catch (Exception ex)
            {
                var dtoResponse = new APIResponse
                {
                    ErrorMessages = new List<String> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dtoResponse);
                var apiResult = JsonConvert.DeserializeObject<T>(res);

                return apiResult!;
            }

        }
    }
}
