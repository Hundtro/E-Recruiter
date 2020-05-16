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

            dbAdapter.ExecuteSelectCommand(SqlProcedures.SelectReminders());
            while(dbAdapter.MoveToNextRow())
            {
                Reminder reminder = new Reminder();
                reminder.Id = dbAdapter.GetColumnValue("Id");
                reminder.Value = dbAdapter.GetColumnValue("Value");
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
                dbAdapter.ExecuteCommand(SqlProcedures.InsertReminder(text));

            return Redirect("/Home/Index");
        }
    }
}