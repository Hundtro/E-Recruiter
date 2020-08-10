using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace erecruiter
{
    public class ReminderController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public ReminderController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }

        public List<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetReminders());
            while(dbAdapter.MoveToNextRow())
            {
                Reminder reminder = new Reminder();
                reminder.Id = dbAdapter.GetColumnValue("Id");
                reminder.Text = dbAdapter.GetColumnValue("Text");
                reminder.Created = dbAdapter.GetColumnValue("Created");
                reminders.Add(reminder);
            }
            dbAdapter.ClearData();

            return reminders;
        }

        [HttpPost]
        [Route("/InsertReminder")]
        public IActionResult InsertReminder(string text)
        {   
            if(!string.IsNullOrEmpty(text))
                dbAdapter.ExecuteCommand(SqlProcedures.AddReminder(text));

            return Redirect("/Home/Index");
        }

        [HttpPost]
        [Route("/DeleteReminder")]
        public IActionResult DeleteReminder(string Id)
        {
            if(!string.IsNullOrEmpty(Id))
                dbAdapter.ExecuteCommand(SqlProcedures.DeleteReminder(Id));

            return Redirect("/Home/Index");
        }
    }
}
