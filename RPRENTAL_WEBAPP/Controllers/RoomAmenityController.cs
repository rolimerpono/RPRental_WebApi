using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.RoomAmenity;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class RoomAmenityController : Controller
    {

        private readonly IRoomAmenityService _IRoomAmenityService;
        private readonly IMapper _IMapper;
        private readonly IHelperService _helper;
        private readonly ICompositeViewEngine _viewEngine;


        public RoomAmenityController(IRoomAmenityService RoomAmenityService, IMapper Mapper, IHelperService helper, ICompositeViewEngine viewEngine)
        {
            _IRoomAmenityService = RoomAmenityService;
            _IMapper = Mapper;
            _helper = helper;
            _viewEngine = viewEngine;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RoomAmenityDTO> objAmenities = new List<RoomAmenityDTO>();

            try
            {

                var response = await _IRoomAmenityService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    objAmenities = JsonConvert.DeserializeObject<List<RoomAmenityDTO>>(Convert.ToString(response.Result)!)!;

                }

                return Json(new { success = true, message = "", data = objAmenities });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, data = objAmenities });
            }
        }



        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyRoomAmenity(RoomAmenityCreateDTO objRoomAmenity)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }

                var response = await _IRoomAmenityService.CreateAsync<APIResponse>(objRoomAmenity, HttpContext.Session.GetString(SD.TokenSession)!);

                if (response != null && response.IsSuccess)
                {
                    return Json(new { success = true, message = response.Message });
                }
                else
                {
                    return Json(new { success = false, message = response.Message });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }


        }


    }
}
