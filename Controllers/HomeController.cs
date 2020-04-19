using Microsoft.AspNetCore.Mvc;

namespace erecruiter
{
    public class HomeController : Controller
    {
        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/")]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}