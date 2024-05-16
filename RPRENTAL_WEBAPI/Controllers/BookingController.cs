using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{


    [Route("api/Booking")]
    [ApiController]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
