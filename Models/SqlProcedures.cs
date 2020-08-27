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

        public static string GetCandidates(string name, string surname)
        {
            string query = File.ReadAllText("Data/GetCandidates.sql");
            query = query.Replace("?name?", name);
            query = query.Replace("?surname?", surname);
            return query;
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

        public static string GetCandidate(string id)
        {
            string query = File.ReadAllText("Data/GetCandidate.sql");
            query = query.Replace("?id?", id);
            return query;
        }
       

        public static string UpdateCandidate(Candidate candidate)
        {
            string query = File.ReadAllText("Data/UpdateCandidate.sql");
            
            if(candidate.Name != null)
            {
                query = query.Replace("Name = '?name?',", "");
            }
            else
            {
                query = query.Replace("?name?", candidate.Name);
            }

            if(candidate.Surname != null)
            {
                query = query.Replace("Surname = '?surname?',", "");
            }
            else
            {
                query = query.Replace("?surname?", candidate.Surname);
            }

            if(candidate.BirthDate != null)
            {
                query = query.Replace("BirthDate = '?birthdate?',", "");
            }
            else
            {
                query = query.Replace("?birthdate?", candidate.BirthDate);
            }

            query = query.Replace("?gender?", candidate.Gender);
            query = query.Replace("?wantedsalary?", candidate.WantedSalary);
            query = query.Replace("?email?", candidate.Email);
            query = query.Replace("?mobilephone?", candidate.MobilePhone);
            
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
                query = query.Replace("Photo = '?photo?',", "");
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
                query = query.Replace("CVFile = '?cvfile?'", "");
                int lastCommaIndex = query.LastIndexOf(',');
                query = query.Remove(lastCommaIndex, 1);
            }

            query = query.Replace("?id?", candidate.Id);
            
            return query;
        } 
        
        public static string DeleteCandidate(string candidateId)
        {
            string query = File.ReadAllText("Data/DeleteCandidate.sql");
            query = query.Replace("?id?", candidateId);
            Log.Add(query);
            return query;
        }
        
        public static string GetExperiences(string candidateId)
        {
            string query = File.ReadAllText("Data/GetExperiences.sql");
            query = query.Replace("?id?", candidateId);
            return query;
        }

        public static string AddExperience(Experience experience)
        {
            string query = File.ReadAllText("Data/AddExperience.sql");
            query = query.Replace("?candidateId?", experience.CandidateId);
            query = query.Replace("?name?", experience.Name);
            query = query.Replace("?title?", experience.Title);
            query = query.Replace("?startdate?", experience.StartDate);
            query = query.Replace("?enddate?", experience.EndDate);
            return query;
        }
    }
}
