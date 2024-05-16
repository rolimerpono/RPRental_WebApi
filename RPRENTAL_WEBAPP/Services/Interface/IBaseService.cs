
using RPRENTAL_WEBAPP.Models;
using RPRENTAL_WEBAPP.Models.DTO;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IBaseService
    {
        APIResponse _responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);

    }
}
