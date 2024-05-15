using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.ApplicationUsers;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class ApplicationUserService : BaseService, IApplicationUserService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string apiUrl = string.Empty;
        private readonly APIResponse _APIresponse;

        public ApplicationUserService(IHttpClientFactory clientFactory, IConfiguration config ) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = config.GetValue<string>("ApiUrls:RPRentalApi")!;

        }
        public Task<T> LoginAsync<T>(loginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDTO,
                Url = apiUrl + "/api/UserAuth/Login/"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registerRequestDTO,
                Url = apiUrl + "/api/UserAuth/Register/"
            });
        }
    }
}
