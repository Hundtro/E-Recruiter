using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace erecruiter
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            ViewData["Reminders"] = new ReminderController(this.configuration).GetReminders();
            return View();
        }

        [Route("/")]
        public IActionResult Welcome()
        {
            return View();
        }
        
        [Route("/Home/AddReminder")]
        public IActionResult AddReminder()
        {
            return View();
        }
    }
}
