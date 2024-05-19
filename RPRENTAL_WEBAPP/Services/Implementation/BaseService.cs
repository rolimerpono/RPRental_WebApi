using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Services.Interface;
using System.Net.Http.Headers;
using System.Text;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class BaseService : IBaseService
    {
        public APIResponse _responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        public APIResponse responseModel { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _responseModel = new APIResponse();
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("RPRentalApi");
                HttpRequestMessage message = new HttpRequestMessage();

                if (!String.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                message = new HttpRequestMessage
                {
                    RequestUri = new Uri(apiRequest.Url),
                    Method = new HttpMethod(apiRequest.ApiType.ToString())
                };              

                if (apiRequest.ContentType == SD.ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }

                if (apiRequest.ContentType == SD.ContentType.MultipartFormData && apiRequest.Data != null)
                {
                    var content = new MultipartFormDataContent();
                    foreach (var prop in apiRequest.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(apiRequest.Data);
                        if (value is FormFile file && file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                        }
                        else
                        {
                            content.Add(new StringContent(value?.ToString() ?? string.Empty), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else if (apiRequest.ContentType != SD.ContentType.MultipartFormData && apiRequest.Data != null)
                {
                    var jsonContent = JsonConvert.SerializeObject(apiRequest.Data);
                    message.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
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
                    ErrorMessages = new List<String> { Convert.ToString(ex.Message) + " " + SD.SystemMessage.ContactAdmin },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dtoResponse);
                var apiResult = JsonConvert.DeserializeObject<T>(res);

                return apiResult!;
            }

        }
    }
}
