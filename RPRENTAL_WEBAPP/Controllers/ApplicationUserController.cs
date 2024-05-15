using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.ApplicationUsers;
using RPRENTAL_WEBAPP.Services.Interface;
using System.Security.Claims;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{ 
    public class ApplicationUserController : Controller
    {

        private readonly IApplicationUserService _IApplicationUserService;
      

        public ApplicationUserController(IApplicationUserService _AppUserService)
        {
            _IApplicationUserService = _AppUserService;
           
        }


        [HttpGet]
        public IActionResult Login()
        {
            loginRequestDTO loginRequest = new();
        
            return View(loginRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(loginRequestDTO loginRequest)
        {
            APIResponse response = await _IApplicationUserService.LoginAsync<APIResponse> (loginRequest);
            if (response != null && response.IsSuccess)
            {
                LoginResponseDTO objResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result)!)!;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, objResponse.User.UserName!),
                    new Claim(ClaimTypes.Role, objResponse.User.Role!)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var cp = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp);
                HttpContext.Session.SetString(SD.TokenSession, objResponse.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", response!.ErrorMessages.FirstOrDefault()!);
            }

            return View(loginRequest);
        }


        [HttpGet]
        public IActionResult Register()
        {
            RegistrationRequestDTO loginRequest = new();

            return View(loginRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO regRequest)
        {
            APIResponse response = await _IApplicationUserService.RegisterAsync<APIResponse>(regRequest);
            if(response != null && response.IsSuccess)
            {
                return RedirectToAction("Login", "ApplicationUser");
            }

            return View(response);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.TokenSession, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {

            return View();
        }



    }
}
