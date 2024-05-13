using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    public class ApplicationUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
