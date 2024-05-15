using RPRENTAL_WEBAPP.Models.DTO.ApplicationUsers;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IApplicationUserService
    {
        Task<T> LoginAsync<T>(loginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO);
     
    }
}
