﻿using Microsoft.AspNetCore.Mvc;

namespace RPRENTAL_WEBAPI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
