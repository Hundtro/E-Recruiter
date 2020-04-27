using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 

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
            return View();
        }
        [Route("/")]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}