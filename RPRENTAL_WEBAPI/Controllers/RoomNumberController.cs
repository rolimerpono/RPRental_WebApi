using AutoMapper;
using DataServices.Common.DTO;
using DataServices.Common.DTO.RoomNumber;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Net;
using System.Text.Json;
using Utility;

namespace RPRENTAL_WEBAPI.Controllers
{
    [Route("api/RoomNumber")]
    [ApiController]
    public class RoomNumberController : ControllerBase
    {


        private readonly ILogger<RoomController> _logger;
        private APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        private readonly IRoomNumberService _IRoomNumberService;
        public RoomNumberController(ILogger<RoomController> logger, IRoomNumberService IRoomNumberService, IMapper mapper)
        {
            _logger = logger;
            _IRoomNumberService = IRoomNumberService;
            _IMapper = mapper;
            _APIResponse = new APIResponse();
        }

        [Authorize]
        [HttpGet("{RoomNo:int}", Name = "GetRoomNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 5)]
        public async Task<ActionResult<APIResponse>> GetRoomNumber(int RoomNo)
        {
            try
            {
                var response = await _IRoomNumberService.GetAsync(RoomNo);

                if (RoomNo == 0)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    _APIResponse.StatusCode = response.StatusCode;
                    return BadRequest(_APIResponse);

                }

                _APIResponse.Result = _IMapper.Map<RoomNumberDTO>(response.Result);
                _APIResponse.IsSuccess = response.IsSuccess;
                _APIResponse.StatusCode = response.StatusCode;
                _APIResponse.Message = response.Message;
                return Ok(_APIResponse);

            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
            }

            return _APIResponse;
        }

        [Authorize]
        [HttpGet(Name = "GetRoomNumbers")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRoomNumbers(int PageSize = 0, int PageNumber = 1)
        {

            try
            {                

                var response = await _IRoomNumberService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);

                if (response == null)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    _APIResponse.StatusCode = response.StatusCode;                    
                    return NotFound(_APIResponse);
                }

                Pagination _pagination = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(_pagination));
                _APIResponse.Result = _IMapper.Map<List<RoomNumberDTO>>(response.Result);

                _APIResponse.IsSuccess = response.IsSuccess;
                _APIResponse.StatusCode = response.StatusCode;
                _APIResponse.Message = response.Message;

                return Ok(_APIResponse);

            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
            }

            return _APIResponse;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRoomNumber([FromBody] RoomNumberCreateDTO roomNumberDTO)
        {
            {
                try
                {
                    if (roomNumberDTO == null)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }                 

                    var response = await _IRoomNumberService.CreateAsync(roomNumberDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };
                    }                  

                    _APIResponse.IsSuccess = true;
                    _APIResponse.Result = roomNumberDTO;
                    _APIResponse.StatusCode = HttpStatusCode.Created;
                    _APIResponse.Message = SD.CrudTransactionsMessage.Save;

                }
                catch (Exception ex)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
                }

                return _APIResponse;
            }

        }


        [Authorize]
        [HttpPut("{RoomNo:int}", Name = "UpdateRoomNumber")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRoomNumber(int RoomNo, [FromBody] RoomNumberUpdateDTO roomDTO)
        {
            {
                try
                {
                    if (roomDTO == null || RoomNo != roomDTO.RoomId)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }

                    var response = await _IRoomNumberService.UpdateAsync(roomDTO);

                    if (response.IsSuccess)
                    {
                        _APIResponse.IsSuccess = true;
                        _APIResponse.Result = roomDTO;
                        _APIResponse.Message = response.Message;
                        _APIResponse.StatusCode = HttpStatusCode.NoContent;
                        return Ok(_APIResponse);
                    }

                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    _APIResponse.ErrorMessages = new List<string>() { response.Message};

                }
                catch (Exception ex)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.ErrorMessages = new List<string>() { ex.Message };
                }

                return _APIResponse;
            }

        }


        [Authorize]
        [HttpDelete("{RoomNo:int}", Name = "DeleteRoomNumber")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteRoomNumber(int RoomNo)
        {
            {
                try
                {
                    if (RoomNo == 0)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }

                    var response = await _IRoomNumberService.RemoveAsync(RoomNo);

                    if (response.IsSuccess)
                    {
                        _APIResponse.IsSuccess = true;
                        _APIResponse.StatusCode = HttpStatusCode.NoContent;
                        _APIResponse.Message = response.Message;                     
                        return Ok(_APIResponse);
                    }

                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    _APIResponse.ErrorMessages = new List<string>() { response.Message + " " + SD.SystemMessage.ContactAdmin };

                }
                catch (Exception ex)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
                }

                return _APIResponse;
            }

        }

    }
}
