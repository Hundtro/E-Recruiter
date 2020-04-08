using Microsoft.AspNetCore.Mvc;

namespace web
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