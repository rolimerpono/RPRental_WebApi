using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Services.Interface;
using Utility;

namespace RPRENTAL_WEBAPP.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _IRoomService;
        private readonly IMapper _IMapper;

        public RoomController(IRoomService RoomService , IMapper Mapper)
        {
            _IRoomService = RoomService;
            _IMapper = Mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<RoomDTO> objRooms = new List<RoomDTO>();
            var response = await _IRoomService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.TokenSession)!);

            if(response != null && response.IsSuccess)
            {
                objRooms = JsonConvert.DeserializeObject<List<RoomDTO>>(Convert.ToString(response.Result)!)!; 
 
            }      
            
            return View();
        }

       
    }
}
