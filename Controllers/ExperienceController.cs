using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class ExperienceController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public ExperienceController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }
		
        public List<Experience> GetExperiences(string id)
		{
            List<Experience> experiences = new List<Experience>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetExperiences(id));
            while(dbAdapter.MoveToNextRow())
            {
                Experience experience = new Experience();

                experience.Name = dbAdapter.GetColumnValue("Name");
                experience.Title = dbAdapter.GetColumnValue("Title");
                experience.StartDate = dbAdapter.GetColumnValue("StartDate");
                experience.EndDate = dbAdapter.GetColumnValue("EndDate");

                experiences.Add(experience);
            }
            dbAdapter.ClearData();

            return experiences;
        }

        [HttpPost]
        [Route("/InsertExperience")]
        public IActionResult InsertExperience(Experience experience)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddExperience(experience));

            return Redirect("/Home/FindCandidate");
        }
    }
}
