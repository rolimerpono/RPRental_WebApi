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
        private APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        private readonly IRoomService _IRoomService;

        public RoomController(ILogger<RoomController> logger, IRoomService RoomService, IMapper mapper)
        {
            _logger = logger;
            _IRoomService = RoomService;
            _IMapper = mapper;
            _APIResponse = new();
        }  
  
        [HttpGet("{RoomId:int}", Name = "GetRoom")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 5)]
        public async Task<ActionResult<APIResponse>> GetRoom(int RoomId)
        {
            try
            {
                if (RoomId == 0)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                var response = await _IRoomService.GetAsync(RoomId);

                if(response.IsSuccess == false)
                {
                    _APIResponse.StatusCode = response.StatusCode;
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    return BadRequest(_APIResponse);
                }           

                _APIResponse.Result = _IMapper.Map<RoomDTO>(response.Result);
                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_APIResponse);

            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages = new List<string>() { ex.Message  + " " + SD.SystemMessage.ContactAdmin};
            }

            return _APIResponse;
        }



 
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

                var response = await _IRoomService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);
            

                if (response == null)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }
                
                if(response.IsSuccess == false)
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
                _APIResponse.Result = _IMapper.Map<List<RoomDTO>>(response.Result);
                _APIResponse.IsSuccess = true;
                _APIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_APIResponse);

            }
            catch (Exception ex)
            {
                _APIResponse.IsSuccess = false;
                _APIResponse.ErrorMessages = new List<string>() { ex.Message  + " " + SD.SystemMessage.ContactAdmin};
            }

            return _APIResponse;
        }      



        [Authorize]
     
        [HttpPost(Name = "CreateRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRoom([FromForm] RoomCreateDTO roomDTO)
        {
            {
                try
                {
                    if (roomDTO == null)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }


                   var response = await _IRoomService.CreateAsync(roomDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };                   
                        return Ok(_APIResponse);

                    }                  

                    _APIResponse.Result = roomDTO;
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


        [Authorize]
        [HttpPut("{RoomId:int}", Name = "UpdateRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRoom(int RoomId, [FromForm] RoomUpdateDTO roomDTO)
        {
            {
                try
                {
                    if (roomDTO == null || RoomId != roomDTO.RoomId)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }
                 
                    var response = await _IRoomService.UpdateAsync(roomDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };
                    }

                    _APIResponse.IsSuccess = true;
                    _APIResponse.Result = roomDTO;
                    _APIResponse.Message = response.Message;
                    _APIResponse.StatusCode = HttpStatusCode.NoContent;                
                    return Ok(_APIResponse);

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
        [HttpDelete("{RoomId:int}", Name = "DeleteRoom")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteRoom(int RoomId)
        {
            {
                try
                {
                    if (RoomId == 0)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }
                    
                    var response = await _IRoomService.RemoveAsync(RoomId);

                    if (response.IsSuccess)
                    {                       
                        _APIResponse.StatusCode = HttpStatusCode.NoContent;
                        _APIResponse.Message = response.Message;
                        _APIResponse.IsSuccess = true;
                        return Ok(_APIResponse);
                    }

                    _APIResponse.IsSuccess = false;
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    _APIResponse.ErrorMessages = new List<string>() { response.Message + " " + SD.SystemMessage.ContactAdmin };

                }
                catch (Exception ex)
                {
                    _APIResponse.IsSuccess = false;
                    _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin};
                }

                return _APIResponse;
            }

        }
       

    }
}
