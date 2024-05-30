
using Newtonsoft.Json.Linq;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Services.Interface;
using sib_api_v3_sdk.Model;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string apiUrl = string.Empty;

        public RoomService(IHttpClientFactory clientFactory, IConfiguration config) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = config.GetValue<string>("ApiUrls:RPRentalApi")!;
        }

        public Task<T> GetAsync<T>(int RoomId, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Room/" + RoomId,
                ContentType = SD.ContentType.Json

            });
        }


        public Task<T> GetAllAsync<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Room/",
                ContentType = SD.ContentType.Json
            });
        }


        public Task<T> CreateAsync<T>(RoomCreateDTO objRoom , string Token)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.POST,
                Data= objRoom,
                Token = Token,
                Url = apiUrl + "/api/Room/",
                ContentType = SD.ContentType.MultipartFormData                
            });
        }

        public Task<T> UpdateAsync<T>(RoomUpdateDTO objRoom, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objRoom,
                Token = Token,
                Url = apiUrl + "/api/Room/" + objRoom.RoomId,
                ContentType = SD.ContentType.MultipartFormData
            });
        }

        public Task<T> DeleteAsync<T>(int RoomId , string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,               
                Token = Token,
                Url = apiUrl + "/api/Room/" + RoomId,
            });
        }

        public Task<T> GetRoomAvailableAsync<T>(DateOnly CheckInDate, DateOnly CheckoutDate, string Token)
        {
            // Convert DateOnly to string format (assuming "yyyy-MM-dd" format)
            string checkInDateStr = CheckInDate.ToString("yyyy-MM-dd");
            string checkoutDateStr = CheckoutDate.ToString("yyyy-MM-dd");

            // Construct the URL with path parameters
            string urlWithDates = $"{apiUrl}/api/Room/{checkInDateStr}/{checkoutDateStr}";

            // Make the API request
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = urlWithDates,
                ContentType = SD.ContentType.Json
            });
        }
    }
}
