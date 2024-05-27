using Microsoft.AspNetCore.Mvc;
using RPRENTAL_WEBAPP.Models;
using RPRENTAL_WEBAPP.Models.DTO.Home;
using System.Diagnostics;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? iPage)
        {

            //var pageNumber = iPage ?? 1;
            //var pageSize = 6;

            //var objRoomList = _iWorker.tbl_Rooms
            // .GetAll(IncludeProperties: "RoomAmenities")
            // .Select(room_item => new HomeVM
            // {
            //     RoomId = room_item.RoomId,
            //     RoomName = room_item.RoomName,
            //     Description = room_item.Description,
            //     RoomPrice = room_item.RoomPrice,
            //     RoomAmenities = room_item.RoomAmenities?.Select(item => new RoomAmenity
            //     {
            //         Id = item.Id,
            //         RoomId = item.RoomId,
            //         AmenityId = item.AmenityId,
            //         Amenity = _iWorker.tbl_Amenity.Get(fw => fw.AmenityId == item.AmenityId),
            //         Room = item.Room
            //     }).ToList(),
            //     MaxOccupancy = room_item.MaxOccupancy,
            //     ImageUrl = room_item.ImageUrl,
            // }).ToList();

            //return View("Index", PaginatedList<HomeDTO>.Create(objRoomList.AsQueryable(), pageNumber, pageSize));
            return View();
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
