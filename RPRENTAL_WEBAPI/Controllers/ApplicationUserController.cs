using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;


namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/UserAuth")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserService _IApplicationUserService;
        private readonly APIResponse _Response;

        public ApplicationUserController(IApplicationUserService appUserService)
        {
            _IApplicationUserService = appUserService;
            _Response = new APIResponse();

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] loginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _IApplicationUserService.Login(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {

                _Response.StatusCode = HttpStatusCode.BadRequest;
                _Response.IsSuccess = false;
                _Response.ErrorMessages.Add(SD.SystemMessage.FailUserLogin);
                return BadRequest(_Response);
            }

            _Response.StatusCode = HttpStatusCode.OK;
            _Response.IsSuccess = true;
            _Response.ErrorMessages.Add(SD.SystemMessage.Login);
            _Response.Result = loginResponse;
            return Ok(_Response);



        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            bool IsUserUnique = await _IApplicationUserService.IsUniqueUsername(registrationRequestDTO.UserName!);
            if (!IsUserUnique)
            {
                _Response.StatusCode = HttpStatusCode.BadRequest;
                _Response.IsSuccess = false;
                _Response.ErrorMessages.Add(SD.CrudTransactionsMessage.RecordExists);
                return BadRequest(_Response);
            }

            var objUser = await _IApplicationUserService.Register(registrationRequestDTO);
            if (objUser == null)
            {
                _Response.StatusCode = HttpStatusCode.BadRequest;
                _Response.IsSuccess = false;
                _Response.ErrorMessages.Add(SD.CrudTransactionsMessage.InvalidInput);
                return BadRequest(_Response);
            }

            _Response.StatusCode = HttpStatusCode.OK;
            _Response.IsSuccess = true;
            return Ok(_Response);
        }

    }
}
