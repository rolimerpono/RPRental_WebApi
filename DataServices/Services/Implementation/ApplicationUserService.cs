using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Services.Implementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IWorker _IWorker;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        private string JwtKey = string.Empty;

        public ApplicationUserService(IWorker IWorker, UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager,
            RoleManager<IdentityRole> rolemanager, IConfiguration config)
        {
            _IWorker  = IWorker;
            _UserManager = usermanager;
            _SignInManager = signinmanager;
            _RoleManager = rolemanager;
            JwtKey = config.GetValue<string>("JwtSettings:JwtKey")!;
        }

        public async Task<bool> IsUniqueUsername(string username)
        {
            var objUser = await _IWorker.tbl_User.GetAsync(fw => fw.Email!.ToLower() == username.ToLower());

            if (objUser == null)
            {
                return true;
            }

            return false;
           
        }

        public async Task<LoginResponseDTO> Login(loginRequestDTO loginRequestDTO)
        {

            var signInResult = await _SignInManager.PasswordSignInAsync(loginRequestDTO.Username, loginRequestDTO.Password, loginRequestDTO.IsRemember, false);
            var loginResponse = new LoginResponseDTO();

            if (signInResult.Succeeded)
            {
                var user = await _UserManager.FindByEmailAsync(loginRequestDTO.Username);

                if (user == null)
                {

                    return loginResponse = new LoginResponseDTO
                    {
                        Token = string.Empty,
                        User = null
                    };

                }

             
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginResponse = new LoginResponseDTO
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                };

                return loginResponse;                
            }
            else
            {
                return loginResponse = new LoginResponseDTO
                {
                    Token = string.Empty,
                    User = null
                };
            }
        }

        public async Task<ApplicationUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {          
            ApplicationUser objUser = new ApplicationUser()
            {
                Fullname = registrationRequestDTO.User.Fullname,
                Email = registrationRequestDTO.User.Email,
                PhoneNumber = registrationRequestDTO.User.PhoneNumber,
                NormalizedEmail = registrationRequestDTO.User.Email,
                EmailConfirmed = true,
                UserName = registrationRequestDTO.User.Email,
                CreatedDate = DateTime.Now,

            };
                    

            var objUserManager = await _UserManager.CreateAsync(objUser, registrationRequestDTO.User.Password);

            if (objUserManager.Succeeded)
            {
                if (!string.IsNullOrEmpty(registrationRequestDTO.User.Role))
                {
                    await _UserManager.AddToRoleAsync(objUser, registrationRequestDTO.User.Role);
                }
                else
                {
                    await _UserManager.AddToRoleAsync(objUser, SD.UserRole.Customer.ToString());
                }

                objUser.Password = "";
                objUser.ConfirmPassword = "";

                return objUser;
            }

            return objUser;

        }
    }
}
