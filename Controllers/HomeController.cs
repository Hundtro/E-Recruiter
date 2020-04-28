using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace erecruiter
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private DBManager dbmanager;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbmanager = new DBManager(configuration["database"]);
        }

        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            ViewData["Reminders"] = GetReminders();
            return View();
        }
        [Route("/")]
        public IActionResult Welcome()
        {
            return View();
        }

        private List<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder>();

            dbmanager.ExecuteSelectCommand(SqlCommands.SelectReminders());
            while(dbmanager.MoveToNextRow())
            {
                Reminder reminder = new Reminder();
                reminder.Id = dbmanager.GetColumnValue("Id");
                reminder.Value = dbmanager.GetColumnValue("Value");
                reminder.Created = dbmanager.GetColumnValue("Created");
                reminders.Add(reminder);
            }
            dbmanager.ClearData();

            return reminders;
        }
    }
}
