using Azure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Model;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Home;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Services.Interface;
using System.Diagnostics;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoomService _IRoomService;
        private readonly IRoomAmenityService _IRoomAmenityService;
        private readonly IAmenityService _IAmenityService;
        private readonly IHelperService _helper;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly int _PageSize = 8;


        public HomeController(ILogger<HomeController> logger, IAmenityService iAmenityService, IRoomService iRoomService, IRoomAmenityService iRoomAmenityService, IHelperService helper, ICompositeViewEngine viewengine)
        {
            _logger = logger;
            _IAmenityService = iAmenityService;
            _IRoomService = iRoomService;
            _IRoomAmenityService = iRoomAmenityService;
            _helper = helper;
            _viewEngine = viewengine;
        }

        public async Task<IActionResult> Index(int? iPage)
        {

            int pageNumber = iPage ?? 1;           
            APIResponse response = new();


            response = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession));


            if (response != null)
            {
                var objRoomList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)!)!;



                return View("Index", PaginatedList<HomeDTO>.Create(objRoomList.AsQueryable(), pageNumber, _PageSize));
            }

            return View("Index");



        }

        [HttpGet]
        public async Task<IActionResult> GetRoomAvailable(DateOnly CheckinDate, DateOnly CheckoutDate, int? iPage)
        {

            DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
            APIResponse response = new();

            try
            {

                if ((CheckoutDate < CheckinDate || CheckinDate <= dateToday) && iPage == null)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.DateRange });
                }

                response = await _IRoomService.GetRoomAvailableAsync<APIResponse>(CheckinDate,CheckoutDate,HttpContext.Session.GetString(SD.TokenSession));
                

                if (response == null)
                {
                    return Json(new { success = false, message = response.Message });
                }

                var objRoomList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)!)!;
                

                PartialViewResult pvr = PartialView("Common/_RoomList", GetPaginatedRoomList(iPage, objRoomList.AsQueryable()));
                string html_result = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, message = "", htmlContent = html_result });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayRoomAvailable(DateOnly CheckinDate, DateOnly CheckoutDate, int? iPage)
        {

            DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
            APIResponse response = new();

            try
            {

                if ((CheckoutDate < CheckinDate || CheckinDate <= dateToday) && iPage == null)
                {
                    return Json(new { success = false, message = SD.CrudTransactionsMessage.DateRange });
                }

                response = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession));


                if (response == null)
                {
                    return Json(new { success = false, message = response.Message });
                }

                var objRoomList = JsonConvert.DeserializeObject<List<HomeDTO>>(Convert.ToString(response.Result)!)!;


                PartialViewResult pvr = PartialView("Common/_RoomList", GetPaginatedRoomList(iPage, objRoomList.AsQueryable()));
                string html_result = _helper.ViewToString(this.ControllerContext, pvr, _viewEngine);

                return Json(new { success = true, message = "", htmlContent = html_result });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message + " " + SD.SystemMessage.ContactAdmin });
            }
        }

        private PaginatedList<HomeDTO> GetPaginatedRoomList(int? pageNumber, IQueryable<HomeDTO> source = null)
        {          
            return PaginatedList<HomeDTO>.Create(source.AsQueryable(), pageNumber ?? 1, _PageSize);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
