using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class CandidateController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public CandidateController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }
		
        public List<Candidate> GetCandidates(string name, string surname)
		{
			List<Candidate> candidates = new List<Candidate>();

			dbAdapter.ExecuteSelectCommand(SqlProcedures.GetCandidates(name, surname));
			while(dbAdapter.MoveToNextRow())
			{
                Candidate candidate = new Candidate();
                
                candidate.Id = dbAdapter.GetColumnValue("Id");
                candidate.Name = dbAdapter.GetColumnValue("Name");
                candidate.Surname = dbAdapter.GetColumnValue("Surname");
                candidate.BirthDate = dbAdapter.GetColumnValue("BirthDate");
                candidate.Gender = dbAdapter.GetColumnValue("Gender");
                candidate.WantedSalary = dbAdapter.GetColumnValue("WantedSalary");
                candidate.Email = dbAdapter.GetColumnValue("Email");
                candidate.MobilePhone = dbAdapter.GetColumnValue("MobilePhone");
                candidate.HomeOffice = dbAdapter.GetColumnValue("HomeOffice");
                candidate.ExWorker = dbAdapter.GetColumnValue("ExWorker");
                candidate.PhotoData = dbAdapter.GetColumnValue("Photo");
                candidate.CVFileData = dbAdapter.GetColumnValue("CVFile");
                candidate.CreatedBy = dbAdapter.GetColumnValue("CreatedBy");

                candidates.Add(candidate);
			}
			dbAdapter.ClearData();

			return candidates;
        }

        [HttpPost]
        [Route("/InsertCandidate")]
        public IActionResult InsertCandidate(Candidate candidate)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddCandidate(candidate));

            return Redirect("/Home/Index");
        }

        [HttpPost]
        [Route("/FindCandidate")]
        public IActionResult FindCandiddate(string name, string surname)
        {
            return Redirect("/Home/FindCandidate");
        }
    }
}
