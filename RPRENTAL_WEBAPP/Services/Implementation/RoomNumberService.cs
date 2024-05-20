
using Newtonsoft.Json.Linq;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Models.DTO.RoomNumber;
using RPRENTAL_WEBAPP.Services.Interface;
using sib_api_v3_sdk.Model;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class RoomNumberService : BaseService, IRoomNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string apiUrl = string.Empty;

        public RoomNumberService(IHttpClientFactory clientFactory, IConfiguration config) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = config.GetValue<string>("ApiUrls:RPRentalApi")!;
        }

        public Task<T> GetAsync<T>(int RoomNo, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/RoomNumber/" + RoomNo

            });
        }


        public Task<T> GetAllAsync<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/RoomNumber/"
            });
        }


        public Task<T> CreateAsync<T>(RoomNumberCreateDTO objRoom , string Token)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.POST,
                Data= objRoom,
                Token = Token,
                Url = apiUrl + "/api/RoomNumber/",
                ContentType = SD.ContentType.Json                
            });
        }

        public Task<T> UpdateAsync<T>(RoomNumberUpdateDTO objRoom, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objRoom,
                Token = Token,
                Url = apiUrl + "/api/RoomNumber/" + objRoom.RoomId,
                ContentType = SD.ContentType.Json
            });
        }

        public Task<T> DeleteAsync<T>(int RoomNo, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,               
                Token = Token,
                Url = apiUrl + "/api/RoomNumber/" + RoomNo,
            });
        }


       

       
    }
}
