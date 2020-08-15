using System.IO;

namespace erecruiter
{
    class SqlProcedures
    {
        public static string GetUserByLogin(string login, string password)
        {
            string query = File.ReadAllText("Data/GetUserByLogin.sql");
            query = query.Replace("?login?", login);
            query = query.Replace("?password?", password);
            return query;
        }       

        public static string GetReminders()
        {
            string query = File.ReadAllText("Data/GetReminders.sql");
            query = query.Replace("?userId?", Session.UserId);
            return query;
        }

        public static string AddReminder(string text)
        {
            string query = File.ReadAllText("Data/AddReminder.sql");
            query = query.Replace("?text?", text);
            query = query.Replace("?userId?", Session.UserId);
            return query;
        }

        public static string DeleteReminder(string id)
        {
            string query = File.ReadAllText("Data/DeleteReminder.sql");
            query = query.Replace("?id?", id);
            return query;
        }

        public static string GetCandidates()
        {
            return "";
        }
    }
}
