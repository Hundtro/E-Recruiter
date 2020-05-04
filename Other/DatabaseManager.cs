using System.Collections.Generic;

namespace erecruiter
{
    class DatabaseManager
    {
        private DatabaseAdapter adapter;

        public DatabaseManager(string database)
        {
            adapter = new DatabaseAdapter(database);
        }

        public List<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder>();

            adapter.ExecuteSelectCommand(SqlCommands.SelectReminders());
            while(adapter.MoveToNextRow())
            {
                Reminder reminder = new Reminder();
                reminder.Id = adapter.GetColumnValue("Id");
                reminder.Value = adapter.GetColumnValue("Value");
                reminder.Created = adapter.GetColumnValue("Created");
                reminders.Add(reminder);
            }
            adapter.ClearData();

            return reminders;
        }
    }
}