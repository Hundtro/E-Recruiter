using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace erecruiter
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private DatabaseManager dbmanager;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbmanager = new DatabaseManager(configuration["database"]);
        }

        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            ViewData["Reminders"] = dbmanager.GetReminders();
            return View();
        }
        [Route("/")]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
