using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Model;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;
using static System.Net.Mime.MediaTypeNames;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _IRoomService;
        private readonly IMapper _IMapper;
        private readonly IHelperService _helper;
        private readonly ICompositeViewEngine _viewEngine;

        public RoomController(IRoomService RoomService , IMapper Mapper, IHelperService helper, ICompositeViewEngine viewEngine)   
        {
            _IRoomService = RoomService;
            _IMapper = Mapper;
            _helper = helper;
            _viewEngine = viewEngine;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {                    
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RoomDTO> objRooms = new List<RoomDTO>();

            var response = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession)!);

            if (response != null && response.IsSuccess)
            {
                objRooms = JsonConvert.DeserializeObject<List<RoomDTO>>(Convert.ToString(response.Result)!)!;

            }
            
            
            return Json(new { success = true, message = "", data = objRooms });

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RoomDTO objRoom = new();
            try
            {
                objRoom = new RoomDTO { RoomId = 0, Description = "", RoomName = "", RoomPrice = 0, MaxOccupancy = 0, ImageUrl = "https://placehold.co/600x400", CreatedDate = DateTime.Now };

                PartialViewResult pvr = PartialView("Create", objRoom);
                string html_string = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, htmlContent = html_string });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }

        }
        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomDTO objRoom)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }

                var response = await _IRoomService.CreateAsync<APIResponse>(objRoom, HttpContext.Session.GetString(SD.TokenSession)!);

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


        [Authorize]      
        [HttpGet]
        public async Task<IActionResult> Update(int RoomId)
        {
            try
            {
                RoomDTO objRoom;

                var response = await _IRoomService.GetAsync<APIResponse>(RoomId, HttpContext.Session.GetString(SD.TokenSession)!);

                objRoom = JsonConvert.DeserializeObject<RoomDTO>(Convert.ToString(response.Result)!)!;


                PartialViewResult pvr = PartialView("Update", objRoom);
                string html_string = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, message = "", htmlContent = html_string });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }
           

        }


        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoomDTO objRoom, IFormFile Image)
        {
            try
            {
                if(Image == null)
                {
                    ModelState.Remove("Image");
                }

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }

              
                var response = await _IRoomService.UpdateAsync<APIResponse>(objRoom, HttpContext.Session.GetString(SD.TokenSession)!);

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

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int RoomId)
        {
            try
            {               

                if (RoomId == 0)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.RecordNotFound });
                }

                var response = await _IRoomService.DeleteAsync<APIResponse>(RoomId, HttpContext.Session.GetString(SD.TokenSession)!);

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
