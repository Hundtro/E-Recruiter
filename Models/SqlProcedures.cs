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

        public static string GetReminders(string userId)
        {
            string query = File.ReadAllText("Data/GetReminders.sql");
            //query = SetNextParameter(query, userId);
            return query;
        }

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
            string query = "DELETE FROM main.Reminder ";
                   query += "WHERE Id=" + id;

            return query;
        }

		public static string SelectCandidates()
		{
			string query = "SELECT * FROM main.Candidate";

			return query;
		}

		public static string InserCandidate(Candidate candidate)
		{
			string query = "INSERT INTO main.Candidate ";
			//map parameters
			return query;
		}

		public static string DeleleDelete(string Id)
		{
			string query = "";

			return query;
		}
    }
}
