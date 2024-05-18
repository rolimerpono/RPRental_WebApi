
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

        public Task<T> CreateAsync<T>(RoomDTO objRoom , string Token)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.POST,
                Data= objRoom,
                Token = Token,
                Url = apiUrl + "/api/Room/"            
            });
        }

        public Task<T> DeleteAsync<T>(int id , string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,               
                Token = Token,
                Url = apiUrl + "/api/Room/" + id,
            });
        }

        public Task<T> GetAllAsync<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Room/"
            });
        }

        public Task<T> GetAsync<T>(int id, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Room/" + id
               
            });
        }    

        public Task<T> UpdateAsync<T>(RoomDTO objRoom , string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objRoom,
                Token = Token,
                Url = apiUrl + "/api/Room/" + objRoom.RoomId               
            });
        }
    }
}
