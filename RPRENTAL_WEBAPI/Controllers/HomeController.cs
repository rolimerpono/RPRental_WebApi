using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
