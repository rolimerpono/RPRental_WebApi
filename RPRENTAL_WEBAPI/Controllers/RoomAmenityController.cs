using AutoMapper;
using DataServices.Common.DTO;
using DataServices.Common.DTO.Amenity;
using DataServices.Common.DTO.RoomAmenity;
using DataServices.Services.Implementation;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Net;
using System.Text.Json;
using Utility;

namespace RPRENTAL_WEBAPI.Controllers
{
    [Route("api/RoomAmenity")]
    [ApiController]
    public class RoomAmenityController : ControllerBase
    {

        private readonly ILogger<RoomAmenityController> _logger;
        private APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        private readonly IRoomAmenityService _IRoomAmenityService;

        public RoomAmenityController(IRoomAmenityService roomAmenityService, IMapper mapper)
        {
            _IRoomAmenityService = roomAmenityService;
            _IMapper = mapper;
            _APIResponse = new();
            
        }

        [Authorize]
        [HttpGet("{RoomAmenityId:int}", Name = "GetRoomAmenity")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 5)]
        public async Task<ActionResult<APIResponse>> GetRoomAmenity(int RoomAmenityId)
        {
            try
            {
                if (RoomAmenityId == 0)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                var response = await _IRoomAmenityService.GetAsync(RoomAmenityId);

                if (response.IsSuccess == false)
                {
                    _APIResponse.StatusCode = response.StatusCode;
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    return BadRequest(_APIResponse);
                }

                _APIResponse.Result = _IMapper.Map<RoomAmenityDTO>(response.Result);
                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
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
        [HttpGet(Name = "GetRoomAmenities")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRoomAmenities(int PageSize = 0, int PageNumber = 1)
        {

            try
            {

                var response = await _IRoomAmenityService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);


                if (response == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                if (response.IsSuccess == false)
                {
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    _APIResponse.StatusCode = response.StatusCode;
                }

                Pagination _pagination = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(_pagination));
                _APIResponse.Result = _IMapper.Map<List<RoomAmenityDTO>>(response.Result);
                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
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

        [HttpPost(Name = "CreateRoomAmenity")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRoomAmenity([FromForm] RoomAmenityCreateDTO RoomAmenityDTO)
        {
            {
                try
                {
                    if (RoomAmenityDTO == null)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }


                    var response = await _IRoomAmenityService.CreateAsync(RoomAmenityDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };
                        return Ok(_APIResponse);

                    }

                    _APIResponse.Result = RoomAmenityDTO;
                    _APIResponse.IsSuccess = true;
                    _APIResponse.StatusCode = HttpStatusCode.Created;
                    _APIResponse.Message = SD.CrudTransactionsMessage.Save;
                    return BadRequest(_APIResponse);


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
