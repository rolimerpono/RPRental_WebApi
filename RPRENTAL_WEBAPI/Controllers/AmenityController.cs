using AutoMapper;
using Azure;
using DataServices.Common.DTO;
using DataServices.Common.DTO.Amenity;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Net;
using System.Text.Json;
using Utility;

namespace RPRENTAL_WEBAPI.Controllers
{

    [Route("api/Amenity")]
    [ApiController]
    public class AmenityController : ControllerBase
    {


        private readonly ILogger<RoomController> _logger;
        private APIResponse _APIResponse;
        private readonly IMapper _IMapper;
        private readonly IAmenityService _IAmenityService;

        public AmenityController(IAmenityService amenityService, IMapper mapper )
        {
            _IAmenityService = amenityService;
            _IMapper = mapper;
            _APIResponse = new();
        }


        [Authorize]
        [HttpGet("{AmenityId:int}", Name = "GetAmenity")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 5)]
        public async Task<ActionResult<APIResponse>> GetAmenity(int AmenityId)
        {
            try
            {
                if (AmenityId == 0)
                {
                    _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_APIResponse);
                }

                var response = await _IAmenityService.GetAsync(AmenityId);

                if (response.IsSuccess == false)
                {
                    _APIResponse.StatusCode = response.StatusCode;
                    _APIResponse.IsSuccess = response.IsSuccess;
                    _APIResponse.Message = response.Message;
                    return BadRequest(_APIResponse);
                }

                _APIResponse.Result = _IMapper.Map<AmenityDTO>(response.Result);
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
        [HttpGet(Name = "GetAmenities")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAmenities(int PageSize = 0, int PageNumber = 1)
        {

            try
            {

                var response = await _IAmenityService.GetAllAsync(pageSize: PageSize, pageNumber: PageNumber);


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
                _APIResponse.Result = _IMapper.Map<List<AmenityDTO>>(response.Result);
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

        [HttpPost(Name = "CreateAmenity")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAmenity([FromForm] AmenityCreateDTO amenityDTO)
        {
            {
                try
                {
                    if (amenityDTO == null)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }


                    var response = await _IAmenityService.CreateAsync(amenityDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };
                        return Ok(_APIResponse);

                    }

                    _APIResponse.Result = amenityDTO;
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
        [HttpPut("{AmenityId:int}", Name = "AmenityUpdate")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AmenityUpdate(int AmenityId, [FromForm] AmenityUpdateDTO amenityDTO)
        {
            {
                try
                {
                    if (amenityDTO == null || AmenityId != amenityDTO.AmenityId)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }

                    var response = await _IAmenityService.UpdateAsync(amenityDTO);

                    if (response.IsSuccess == false)
                    {
                        _APIResponse.IsSuccess = false;
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        _APIResponse.Message = response.Message;
                        _APIResponse.ErrorMessages = new List<string>() { response.Message };
                    }

                    _APIResponse.IsSuccess = true;
                    _APIResponse.Result = amenityDTO;
                    _APIResponse.Message = response.Message;
                    _APIResponse.StatusCode = HttpStatusCode.OK;
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
        [HttpDelete("{AmenityId:int}", Name = "DeleteAmenity")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteAmenity(int AmenityId)
        {
            {
                try
                {
                    if (AmenityId == 0)
                    {
                        _APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_APIResponse);
                    }

                    var response = await _IAmenityService.RemoveAsync(AmenityId);

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
                    _APIResponse.ErrorMessages = new List<string>() { ex.Message + " " + SD.SystemMessage.ContactAdmin };
                }

                return _APIResponse;
            }

        }

    }
}
