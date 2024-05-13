using DataServices.Common.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IApplicationUserService
    {
        Task<Boolean> IsUniqueUsername(string username);

        Task<LoginResponseDTO> Login(loginRequestDTO loginRequestDTO);

        Task<ApplicationUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
