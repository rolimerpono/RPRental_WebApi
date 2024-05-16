using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    [Route("api/RoomNumber")]
    [ApiController]
    public class RoomNumber : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
