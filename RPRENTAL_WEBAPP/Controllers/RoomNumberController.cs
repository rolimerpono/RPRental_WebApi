using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Model;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Models.DTO.RoomNumber;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;
using static System.Net.Mime.MediaTypeNames;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class RoomNumberController : Controller
    {

        private readonly IRoomService _IRoomService;
        private readonly IRoomNumberService _IRoomNumberService;
        private readonly IMapper _IMapper;
        private readonly IHelperService _helper;
        private readonly ICompositeViewEngine _viewEngine;

        public RoomNumberController(IRoomNumberService IRoomNumberService, IRoomService IRoomService , IMapper Mapper, IHelperService helper, ICompositeViewEngine viewEngine)   
        {
            _IRoomNumberService = IRoomNumberService;
            _IRoomService = IRoomService;
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
            List<RoomNumberDTO> objRoomNos = new List<RoomNumberDTO>();

            var response = await _IRoomNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession)!);

            if (response != null && response.IsSuccess)
            {
                objRoomNos = JsonConvert.DeserializeObject<List<RoomNumberDTO>>(Convert.ToString(response.Result)!)!;

            }            
            
            return Json(new { success = true, message = "", data = objRoomNos });

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RoomNumberCreateDTO objRoomNo = new();
            try
            {

                var objRoomList = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession));

                var roomList = JsonConvert.DeserializeObject<List<RoomDTO>>(Convert.ToString(objRoomList.Result)!);

                var selectedListItems = roomList!.Select(room =>
                new SelectListItem
                {
                    Value = room.RoomId.ToString(),
                    Text = room.RoomName.ToString()

                })
                .OrderBy(fw => fw.Text)
                .GroupBy(fw => fw.Text)
                .Select(fw => fw.First()).ToList();

                objRoomNo.RoomList = selectedListItems;



                PartialViewResult pvr = PartialView("Create", objRoomNo);
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
        public async Task<IActionResult> Create(RoomNumberCreateDTO objRoomNo)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }

                var response = await _IRoomNumberService.CreateAsync<APIResponse>(objRoomNo, HttpContext.Session.GetString(SD.TokenSession)!);

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
        public async Task<IActionResult> Update(int RoomNo)
        {
            try
            {
                RoomNumberUpdateDTO objRoomNo = new();
              

                var response = await _IRoomNumberService.GetAsync<APIResponse>(RoomNo, HttpContext.Session.GetString(SD.TokenSession)!);

                objRoomNo = JsonConvert.DeserializeObject<RoomNumberUpdateDTO>(Convert.ToString(response.Result)!)!;


                var objRoomList = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession));

                var roomList = JsonConvert.DeserializeObject<List<RoomDTO>>(Convert.ToString(objRoomList.Result)!);

                var selectedListItems = roomList!.Select(room =>
                new SelectListItem
                {
                    Value = room.RoomId.ToString(),
                    Text = room.RoomName.ToString()

                })
                .OrderBy(fw => fw.Text)
                .GroupBy(fw => fw.Text)
                .Select(fw => fw.First()).ToList();

                objRoomNo.RoomList = selectedListItems;


                PartialViewResult pvr = PartialView("Update", objRoomNo);
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
        public async Task<IActionResult> Update(RoomNumberUpdateDTO objRoomNo)
        {
            try
            {
              
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.InvalidInput });
                }               


                var response = await _IRoomNumberService.UpdateAsync<APIResponse>(objRoomNo, HttpContext.Session.GetString(SD.TokenSession)!);

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
        public async Task<IActionResult> Delete(int RoomNo)
        {
            try
            {               

                if (RoomNo == 0)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.RecordNotFound });
                }

                var response = await _IRoomNumberService.DeleteAsync<APIResponse>(RoomNo, HttpContext.Session.GetString(SD.TokenSession)!);

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
