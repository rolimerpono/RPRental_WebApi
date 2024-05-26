using Model;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Amenity;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class AmenityService : BaseService, IAmenityService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string apiUrl = string.Empty;

        public AmenityService(IHttpClientFactory clientFactory, IConfiguration config) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = config.GetValue<string>("ApiUrls:RPRentalApi")!;
        }

        public Task<T> CreateAsync<T>(AmenityCreateDTO objAmenity, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = objAmenity,
                Token = Token,
                Url = apiUrl + "/api/Amenity/",
                ContentType = SD.ContentType.Json
            });
        }

        public Task<T> DeleteAsync<T>(int AmenityId, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Token = Token,
                Url = apiUrl + "/api/Amenity/" + AmenityId,
            });
        }

        public Task<T> GetAllAsync<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Amenity/",
                ContentType = SD.ContentType.Json
            });
        }

        public Task<T> GetAsync<T>(int AmenityId, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Token = Token,
                Url = apiUrl + "/api/Amenity/" + AmenityId,
                ContentType = SD.ContentType.Json

            });
        }

        public Task<T> UpdateAsync<T>(AmenityUpdateDTO objAmenity, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = objAmenity,
                Token = Token,
                Url = apiUrl + "/api/Amenity/" + objAmenity.AmenityId,
                ContentType = SD.ContentType.Json
            });
        }
    }
}
