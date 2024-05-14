
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

        public Task<T> AnyAsync<T>(RoomDTO objRoom)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync<T>(RoomDTO objRoom)
        {
            return SendAsync<T>(new APIRequest() 
            { 
                ApiType = SD.ApiType.POST,
                Data= objRoom,
                Url = apiUrl + "/api/Room/"            
            });;
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,             
                Url = apiUrl + "/api/Room/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,              
                Url = apiUrl + "/api/Room/"
            }); ;
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,              
                Url = apiUrl + "/api/Room/" + id
            }); ;
        }

        public Task<T> UpdateAsync<T>(RoomDTO objRoom)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objRoom,
                Url = apiUrl + "/api/Room/" + objRoom.RoomId
            }); ;
        }
    }
}
