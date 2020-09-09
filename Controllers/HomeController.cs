using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

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
                ViewData["CanConfig"] = Session.CanConfig;
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

        [Route("/Home/Configuration")]
        public IActionResult Configuration()
        {
            if(Session.isLogged)
            {
                Configuration config = new Configuration();
                config.database = this.configuration["database"];
                config.emailAdress = this.configuration["emailAdress"];
                config.emailPassword = this.configuration["emailPassword"];
                config.smtpHost = this.configuration["smtpHost"];
                config.smtpPort = this.configuration["smtpPort"];

                ViewData["Configuration"] = config;
                ViewData["Users"] = new UserController(this.configuration).GetUsers();
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
        
        [Route("/Home/AddUser")]
        public IActionResult AddUser()
        {
            if(Session.isLogged)
                return View();
            else
                return Redirect("/");
        }

        [Route("/Home/UpdateUser")]
        public IActionResult UpdateUser(string id)
        {
            if(Session.isLogged)
            {
                ViewData["User"] = new UserController(this.configuration).GetUserById(id);
                return View();
            }
            else
            {
                return Redirect("/");
            }   
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

        [Route("/Home/UpdateCandidate")]
        public IActionResult UpdateCandidate(string id)
       	{
            if(Session.isLogged)
            { 
                ViewData["Candidate"] = new CandidateController(this.configuration).GetCandidate(id);
                return View();
            }
            else
            {
                return Redirect("/");
            }
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

        [Route("/Home/ManageRecruitment")]
        public IActionResult ManageRecruitment()
        {
            if(Session.isLogged)
            {
                ViewData["HireProcesses"] = new HireProcessController(this.configuration).GetHireProcesses();
                ViewData["JobTitles"] = new JobTitleController(this.configuration).GetJobTitles();    
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Home/AddJobTitle")]
        public IActionResult AddJobTitle()
        {
            if(Session.isLogged)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Home/UpdateJobTitle")]
        public IActionResult UpdateJobTitle(string id)
        {
            if(Session.isLogged)
            {
                ViewData["JobTitle"] = new JobTitleController(this.configuration).GetJobTitle(id); 
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Home/AddHireStep")]
        public IActionResult AddHireStep(string jobTitleId, string jobTitleName)
        {
            if(Session.isLogged)
            {
                ViewData["jobTitleId"] = jobTitleId;
                ViewData["jobTitleName"] = jobTitleName;
                return View();
            }
            else
            {
                return Redirect("/"); 
            }
        }
        
        [Route("/Home/NewHireProcess")]
        public IActionResult NewHireProcess()
        {
            if(Session.isLogged)
            {
                ViewData["Candidates"] = new CandidateController(this.configuration).GetCandidates("", "");
                ViewData["JobTitles"] = new JobTitleController(this.configuration).GetJobTitles();
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Home/FindProcess")]
        public IActionResult FindProcess()
        {
            if(Session.isLogged)
            {
               ViewData["HireProcesses"] = new HireProcessController(this.configuration).FindProcess("", "");
               return View();
            }
            else
            {
               return Redirect("/");
            }
        }

        [Route("/Home/ContactCandidate")]
        public IActionResult ContactCandidate(string title, string text)
        {
            if(Session.isLogged)
            {
                if(String.IsNullOrEmpty(title))
                    title = "Subject...";
                if(String.IsNullOrEmpty(text))
                    text = "Message text...";
 
                ViewData["DefaultTitle"] = title;
                ViewData["DefaultText"] = text;
                ViewData["EmailTemplates"] = new EmailTemplateController(this.configuration).GetEmailTemplates();
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
       
        [HttpPost]
        [Route("/SendEmail")]
        public IActionResult SendEmail(string subject, string text, string adress)
        {
            try
            {
                EmailSender.Send(subject, text, adress, this.configuration);

                return Redirect("/Home");
            }
            catch(System.Exception e)
            {
                Log.Add(e.Message);
                ViewData["ErrorText"] = "An error occured while sending email";
                return View("~/Views/Shared/_Error.cshtml");
            }
        }

        [Route("/Home/AddEmailTemplate")]
        public IActionResult AddEmailTemplate()
        {
            if(Session.isLogged)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Home/UpdateEmailTemplate")]
        public IActionResult UpdateEmailTemplate(string id)
        {
            if(Session.isLogged)
            {
                ViewData["EmailTemplate"] = new EmailTemplateController(this.configuration).GetEmailTemplate(id); 
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("/Logout")]
        public IActionResult Logout()
        {
            Session.isLogged = false;
            return Redirect("/");
        }
    }
}
