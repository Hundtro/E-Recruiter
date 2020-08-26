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

        [Route("/CheckLogin")]
        public IActionResult CheckLogin(string login, string password)
        {
            Session.Login = login;
            Session.Password = password;
            
            bool isLoginSucces = new LoginController(this.configuration).Login();
            
            if(isLoginSucces)
            {
                Session.isLogged = true;
                return Redirect("/Home");
            }
            else
            {
                return Redirect("/LoginError");
            }
        }

        [Route("/LoginError")]
        public IActionResult LoginError()
        {
            return View();
        }

        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            if(Session.isLogged)
            {
                ViewData["UserName"] = Session.FullName;
                ViewData["Reminders"] = new ReminderController(this.configuration).GetReminders();
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/")]
        public IActionResult Login()
        {
            if(Session.isLogged)
                return Redirect("/Home");
            else
                return View(); 
        }
        
        [Route("/Home/AddReminder")]
        public IActionResult AddReminder()
        {
            if(Session.isLogged)
                return View();
            else
                return Redirect("/");
        }

		[Route("/Home/NewCandidate")]
		public IActionResult NewCandidate()
		{
            if(Session.isLogged)    
                return View();
            else
                return Redirect("/");
        }

        [Route("/Home/FindCandidate")]
        public IActionResult FindCandidate()
        {
            if(Session.isLogged)
            {
               ViewData["Candidates"] = new CandidateController(this.configuration).GetCandidates("", "");
               return View();
            }
            else
            {
               return Redirect("/");
            }
        }
        
        [Route("/Home/AddExperience")]
        public IActionResult AddExperience(string candidateId)
        {
            if(Session.isLogged)
            { 
                ViewData["CandidateId"] = candidateId;
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //another actions
        
        [Route("/Logout")]
        public IActionResult Logout()
        {
            Session.isLogged = false;
            return Redirect("/");
        }
    }
}
