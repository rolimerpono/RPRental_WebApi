using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/Room")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserService _IApplicationUserService;

        public ApplicationUserController(IApplicationUserService appUserService)
        {
            _IApplicationUserService = appUserService;
        }
        public IActionResult Index()
        {
           
           throw new  NotImplementedException();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] loginRequestDTO loginRequestDTO )        
        {
            var loginResponse = await _IApplicationUserService.Login(loginRequestDTO);
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {

            return View();
        }

    }
}
