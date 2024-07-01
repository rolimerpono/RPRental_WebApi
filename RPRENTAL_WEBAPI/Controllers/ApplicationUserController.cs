using DataServices.Common.DTO;
using DataServices.Common.DTO.ApplicationUsers;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;


namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/UserAuth")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _IApplicationUserService;
        private readonly APIResponse _APIResponse;

        public ApplicationUserController(IApplicationUserService appUserService)
        {
            _IApplicationUserService = appUserService;
            _APIResponse = new();

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] loginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _IApplicationUserService.Login(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {

                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages.Add(SD.SystemMessage.FailUserLogin);
                return BadRequest(_APIResponse);
            }

            _APIResponse.StatusCode = HttpStatusCode.OK;
            _APIResponse.IsSuccess = true;
            _APIResponse.ErrorMessages.Add(SD.SystemMessage.Login);
            _APIResponse.Result = loginResponse;
            return Ok(_APIResponse);



        }

        [HttpPost("Register")]

        public async Task<ActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)     
        {
            bool IsUserUnique = await _IApplicationUserService.IsUniqueUsername(registrationRequestDTO.UserName!);
            if (!IsUserUnique)
            {
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages.Add(SD.CrudTransactionsMessage.RecordExists);
                return BadRequest(_APIResponse);
            }

            var objUser = await _IApplicationUserService.Register(registrationRequestDTO);
            if (objUser == null)
            {
                _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages.Add(SD.CrudTransactionsMessage.InvalidInput);
                return BadRequest(_APIResponse);
            }

            _APIResponse.StatusCode = HttpStatusCode.OK;
            _APIResponse.IsSuccess = true;
            _APIResponse.Result = objUser;
            return Ok(_APIResponse);
        }

    }
}
