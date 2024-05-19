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
        private APIResponse _Response;
        private readonly IMapper _IMapper;
        private readonly IRoomNumberService _IRoomNumberService;
        public RoomNumberController(ILogger<RoomController> logger, IRoomNumberService IRoomNumberService, IMapper mapper)
        {
            _logger = logger;
            _IRoomNumberService = IRoomNumberService;
            _IMapper = mapper;
            _Response = new APIResponse();
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
                var objRoom = await _IRoomNumberService.GetAsync(RoomNo);

                if (RoomNo == 0)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_Response);
                }

                if (objRoom == null)
                {
                    _Response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_Response);
                }

                _Response.Result = _IMapper.Map<RoomNumberDTO>(objRoom);
                _Response.IsSuccess = true;
                _Response.StatusCode = HttpStatusCode.OK;
                return Ok(_Response);

            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
            }

            return _Response;
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
                IEnumerable<RoomNumber> objRooms = new List<RoomNumber>();

                objRooms = await _IRoomNumberService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);

                if (objRooms == null)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_Response);
                }

                if (objRooms == null)
                {
                    _Response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_Response);
                }

                Pagination _pagination = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(_pagination));
                _Response.Result = _IMapper.Map<List<RoomNumberDTO>>(objRooms);
                _Response.IsSuccess = true;
                _Response.StatusCode = HttpStatusCode.OK;
                return Ok(_Response);

            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
            }

            return _Response;
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
                        _Response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_Response);
                    }

                    var objRoom = _IMapper.Map<RoomNumber>(roomNumberDTO);

                    var response = await _IRoomNumberService.CreateAsync(objRoom);

                    if (response.IsSuccess)
                    {
                        _Response.Result = objRoom;
                        _Response.IsSuccess = true;
                        _Response.StatusCode = HttpStatusCode.Created;
                        _Response.Message = SD.CrudTransactionsMessage.Save;
                        return CreatedAtRoute("GetRoom", new { Id = objRoom.RoomId }, _Response);
                    }

                    _Response.IsSuccess = false;
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.Message = response.Message;
                    _Response.ErrorMessages = new List<string>() { response.Message + " " + SD.SystemMessage.ContactAdmin };

                }
                catch (Exception ex)
                {
                    _Response.IsSuccess = false;
                    _Response.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
                }

                return _Response;
            }

        }

    }
}
