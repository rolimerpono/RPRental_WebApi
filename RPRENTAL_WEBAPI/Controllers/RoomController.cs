using AutoMapper;
using Azure;
using DataServices.Common.DTO;
using DataServices.Common.DTO.Room;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Model;
using System.Net;
using System.Text.Json;
using Utility;




namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {


        private readonly ILogger<RoomController> _logger;
        private APIResponse _Response;
        private readonly IMapper _IMapper;
        private readonly IRoomService _IRoomService;

        public RoomController(ILogger<RoomController> logger, IRoomService RoomService, IMapper mapper)
        {
            _logger = logger;
            _IRoomService = RoomService;
            _IMapper = mapper;
            _Response = new APIResponse();
        }  


        [Authorize]
        [HttpGet("{id:int}", Name = "GetRoom")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 5)]
        public async Task<ActionResult<APIResponse>> GetRoom(int id)
        {
            try
            {
                var objRoom = await _IRoomService.GetAsync(id);

                if (id == 0)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_Response);
                }

                if (objRoom == null)
                {
                    _Response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_Response);
                }

                _Response.Result = _IMapper.Map<RoomDTO>(objRoom);
                _Response.IsSuccess = true;
                _Response.StatusCode = HttpStatusCode.OK;
                return Ok(_Response);

            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = new List<string>() { ex.Message  + " " + SD.SystemMessage.ContactAdmin};
            }

            return _Response;
        }



        [Authorize]
        [HttpGet(Name = "GetRooms")]        
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRooms(int PageSize = 0, int PageNumber = 1)
        {
           
            try
            {
                IEnumerable<Room> objRooms = new List<Room>();

                objRooms = await _IRoomService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);

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
                _Response.Result = _IMapper.Map<List<RoomDTO>>(objRooms);
                _Response.IsSuccess = true;
                _Response.StatusCode = HttpStatusCode.OK;
                return Ok(_Response);

            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = new List<string>() { ex.Message  + " " + SD.SystemMessage.ContactAdmin};
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
        public async Task<ActionResult<APIResponse>> CreateRoom([FromBody] RoomCreateDTO roomDTO)
        {
            {
                try
                {
                    if (roomDTO == null)
                    {
                        _Response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_Response);
                    }

                    var objRoom = _IMapper.Map<Room>(roomDTO);

                   var response = await _IRoomService.CreateAsync(objRoom);

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


        [Authorize]
        [HttpPut("{id:int}", Name = "UpdateRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRoom(int id, [FromBody] RoomUpdateDTO roomDTO)
        {
            {
                try
                {
                    if (roomDTO == null || id != roomDTO.RoomId)
                    {
                        _Response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_Response);
                    }

                    var objRoom = _IMapper.Map<Room>(roomDTO);

                    var response = await _IRoomService.UpdateAsync(objRoom);

                    if (response.IsSuccess)
                    {
                        _Response.Result = objRoom;
                        _Response.Message = response.Message;
                        _Response.StatusCode = HttpStatusCode.NoContent;
                        _Response.IsSuccess = true;
                        return Ok(_Response);
                    }

                    _Response.IsSuccess = false;
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.ErrorMessages = new List<string>() { response.Message + " " + SD.SystemMessage.ContactAdmin };

                }
                catch (Exception ex)
                {
                    _Response.IsSuccess = false;
                    _Response.ErrorMessages = new List<string>() { ex.Message };
                }

                return _Response;
            }

        }


        [Authorize]
        [HttpDelete("{id:int}", Name = "DeleteRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteRoom(int id)
        {
            {
                try
                {
                    if (id == 0)
                    {
                        _Response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_Response);
                    }
                    
                    var response = await _IRoomService.RemoveAsync(id);

                    if (response.IsSuccess)
                    {                       
                        _Response.StatusCode = HttpStatusCode.NoContent;
                        _Response.Message = response.Message;
                        _Response.IsSuccess = true;
                        return Ok(_Response);
                    }

                    _Response.IsSuccess = false;
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.ErrorMessages = new List<string>() { response.Message + " " + SD.SystemMessage.ContactAdmin };

                }
                catch (Exception ex)
                {
                    _Response.IsSuccess = false;
                    _Response.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin};
                }

                return _Response;
            }

        }


        [Authorize]
        [HttpPatch("{id:int}", Name = "UpdatePartialRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialRoom(int id, JsonPatchDocument<RoomUpdateDTO> roomDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.IsSuccess = false;
                    return _Response;
                }

                if (roomDTO == null || id == 0)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.IsSuccess = false;
                    return _Response;
                }

                var ojbRoom = await _IRoomService.GetAsync(id);
                RoomUpdateDTO objData = _IMapper.Map<RoomUpdateDTO>(ojbRoom);

                if (ojbRoom == null)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.IsSuccess = false;
                    return _Response;
                }

                roomDTO.ApplyTo(objData, ModelState);
                Room model = _IMapper.Map<Room>(objData);

                var response = await _IRoomService.UpdateAsync(model);

                if (response.IsSuccess)
                {
                    _Response.StatusCode = HttpStatusCode.NoContent;
                    _Response.IsSuccess = true;
                    _Response.Result = model;

                    return _Response;
                }
            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _Response;
        }

    }
}
