using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    [Route("api/RoomAmenity")]
    [ApiController]
    public class RoomAmenityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
