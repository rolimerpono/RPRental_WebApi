using AutoMapper;
using DataServices.Common.DTO;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Model;
using System.Net;




namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/RoomAPI")]
    [ApiController]
    public class RoomAPIController : ControllerBase
    {


        //private readonly ILogger<RoomAPIController> _logger;
        //private APIResponse _Response;
        //private readonly IMapper _IMapper;
        //private readonly IRoomService _IRoomService;

        //public RoomAPIController(ILogger<RoomAPIController> logger, IRoomService RoomService, IMapper mapper)
        //{
        //    _logger = logger;
        //    _IRoomService = RoomService;
        //    _IMapper = mapper;
        //    _Response = new APIResponse();
        //}

        //[HttpGet("{id:int}", Name = "GetRoom")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ResponseCache(Duration = 5)]
        //public async Task<ActionResult<APIResponse>> GetRoom(int id)
        //{
        //    try
        //    {
        //        var objRoom = await _IRoomService.GetAsync(id);

        //        if (id == 0)
        //        {
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(_Response);
        //        }

        //        if (objRoom == null)
        //        {
        //            _Response.StatusCode = HttpStatusCode.NotFound;
        //            return NotFound(_Response);
        //        }

        //        _Response.Result = _IMapper.Map<RoomDTO>(objRoom);
        //        _Response.StatusCode = HttpStatusCode.OK;
        //        return Ok(_Response);

        //    }
        //    catch (Exception ex)
        //    {
        //        _Response.IsSuccess = false;
        //        _Response.ErrorMessages = new List<string>() { ex.Message };
        //    }

        //    return _Response;
        //}


        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateRoom([FromBody]RoomCreateDTO roomDTO)
        //{
        //    {
        //        try
        //        {
        //            if (roomDTO == null)
        //            {
        //                _Response.StatusCode = HttpStatusCode.BadRequest;
        //                return BadRequest(_Response);
        //            }

        //            var objRoom = _IMapper.Map<Room>(roomDTO);

        //            bool is_success = await _IRoomService.CreateAsync(objRoom);

        //            if (is_success)
        //            {
        //                _Response.Result = objRoom;
        //                _Response.IsSuccess = true;
        //                _Response.StatusCode = HttpStatusCode.Created;
        //                return CreatedAtRoute("GetRoom", new { Id = objRoom.RoomId }, _Response);
        //            }

        //            _Response.IsSuccess = false;
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            _Response.ErrorMessages = new List<string>() { "Room name already exists." };

        //        }
        //        catch (Exception ex)
        //        {
        //            _Response.IsSuccess = false;
        //            _Response.ErrorMessages = new List<string>() { ex.Message };
        //        }

        //        return _Response;
        //    }

        //}

      
        //[HttpPut("{id:int}", Name = "UpdateRoom")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse>> UpdateRoom(int id, [FromBody] RoomUpdateDTO roomDTO)
        //{
        //    {
        //        try
        //        {
        //            if (roomDTO == null || id != roomDTO.RoomId )
        //            {
        //                _Response.StatusCode = HttpStatusCode.BadRequest;
        //                return BadRequest(_Response);
        //            }

        //            var objRoom = _IMapper.Map<Room>(roomDTO);

        //            bool is_success = await _IRoomService.UpdateAsync(objRoom);

        //            if (is_success)
        //            {
        //                _Response.Result = objRoom;
        //                _Response.StatusCode = HttpStatusCode.NoContent;
        //                _Response.IsSuccess = true;
        //                return Ok(_Response);                     
        //            }

        //            _Response.IsSuccess = false;
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            _Response.ErrorMessages = new List<string>() { "Room not exists." };

        //        }
        //        catch (Exception ex)
        //        {
        //            _Response.IsSuccess = false;
        //            _Response.ErrorMessages = new List<string>() { ex.Message };
        //        }

        //        return _Response;
        //    }

        //}


        //[HttpPatch("{id:int}", Name = "UpdatePartialRoom")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse>> UpdatePartialRoom(int id, JsonPatchDocument<RoomUpdateDTO> roomDTO)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            _Response.IsSuccess = false;
        //            return _Response;
        //        }

        //        if (roomDTO == null || id == 0)
        //        {
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            _Response.IsSuccess = false;
        //            return _Response;
        //        }

        //        var ojbRoom = await _IRoomService.GetAsync(id);
        //        RoomUpdateDTO objData = _IMapper.Map<RoomUpdateDTO>(ojbRoom);

        //        if (ojbRoom == null)
        //        {
        //            _Response.StatusCode = HttpStatusCode.BadRequest;
        //            _Response.IsSuccess = false;
        //            return _Response;
        //        }

        //        roomDTO.ApplyTo(objData, ModelState);
        //        Room model = _IMapper.Map<Room>(objData);

        //        bool is_success = await _IRoomService.UpdateAsync(model);

        //        if (is_success)
        //        {
        //            _Response.StatusCode = HttpStatusCode.NoContent;
        //            _Response.IsSuccess = true;
        //            _Response.Result = model;

        //            return _Response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _Response.IsSuccess = false;
        //        _Response.ErrorMessages = new List<string>() { ex.Message };
        //    }

        //    return _Response;
        //}

    }
}
