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

        public static string AddCandidate(Candidate candidate)
        {
            string query = File.ReadAllText("Data/AddCandidate.sql");
            query = query.Replace("?name?", candidate.Name);
            query = query.Replace("?surname?", candidate.Surname);
            query = query.Replace("?birthdate?", candidate.BirthDate);
            query = query.Replace("?gender?", candidate.Gender);
           
            if(candidate.Email != null)
                query = query.Replace("?email?", candidate.Email);
            else
                query = query.Replace("?email?", "");

            if(candidate.MobilePhone != null)
                query = query.Replace("?mobilephone?", candidate.MobilePhone);
            else
                query = query.Replace("?mobilephone?", "");

            if(candidate.WantedSalary != null)
                query = query.Replace("?wantedsalary?", candidate.WantedSalary);
            else
                query = query.Replace("?wantedsalary?", "");
            
            if(candidate.HomeOffice != null && candidate.HomeOffice.Equals("on"))
                query = query.Replace("?homeoffice?", "true");
            else
                query = query.Replace("?homeoffice?", "false");

            if(candidate.ExWorker != null && candidate.ExWorker.Equals("on"))
                query = query.Replace("?exworker?", "true");
            else
                query = query.Replace("?exworker?", "false");
           
            if(candidate.Photo != null)
            {
                MemoryStream stream = new MemoryStream();
                candidate.Photo.CopyTo(stream);
                byte[] ar = stream.ToArray();
                query = query.Replace("?photo?", System.Convert.ToBase64String(ar));
            }
            else
            {
                query = query.Replace("?photo?", "");
            }

            if(candidate.CVFile != null)
            {
                MemoryStream stream = new MemoryStream();
                candidate.CVFile.CopyTo(stream);
                byte[] ar = stream.ToArray();
                query = query.Replace("?cvfile?", System.Convert.ToBase64String(ar));
            }
            else
            {
                query = query.Replace("?cvfile?", "");
            }

            query = query.Replace("?userid?", Session.UserId);
            return query;
        }
    }
}
