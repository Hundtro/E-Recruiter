namespace erecruiter
{
    class SqlProcedures
    {
        public static string SelectReminders()
        {
            string query = "SELECT * FROM main.Reminder";

            return query;
        }

        public static string InsertReminder(string value)
        {
            string query = "INSERT INTO main.Reminder ";
                   query += "(Value, Created) VALUES ('";
                   query += value + "', ";
                   query += "date('now'))";
            
            return query;
        }

        public static string DeleteReminder(string id)
        {
            string query = "DELETE main.Reminder ";
                   query += "WHERE Id=" + id;

            return query;
        }
    }
}