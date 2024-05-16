using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
