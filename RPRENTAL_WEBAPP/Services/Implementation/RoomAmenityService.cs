using Model;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.RoomAmenity;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class RoomAmenityService : BaseService, IRoomAmenityService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string apiUrl = string.Empty;

        public RoomAmenityService(IHttpClientFactory clientFactory, IConfiguration config) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = config.GetValue<string>("ApiUrls:RPRentalApi")!;
        }

        public Task<T> CreateAsync<T>(RoomAmenityCreateDTO objRoomAmenity, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = objRoomAmenity,
                Token = Token,
                Url = apiUrl + "/api/RoomAmenity/",
                ContentType = SD.ContentType.Json
            });
        }

        public Task<T> DeleteAsync<T>(int RoomId, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Token = Token,
                Url = apiUrl + "/api/RoomAmenity/" + RoomId,
            });
        }

        public Task<T> GetAllAsync<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/RoomAmenity/",
                ContentType = SD.ContentType.Json
            });
        }

        public Task<T> GetAsync<T>(int RoomId, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/RoomAmenity/" + RoomId,
                ContentType = SD.ContentType.Json

            });
        }

        public Task<T> UpdateAsync<T>(RoomAmenityUpdateDTO objRoomAmenity, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objRoomAmenity,
                Token = Token,
                Url = apiUrl + "/api/RoomAmenity/" + objRoomAmenity.RoomId,
                ContentType = SD.ContentType.Json
            });
        }
    }
}
