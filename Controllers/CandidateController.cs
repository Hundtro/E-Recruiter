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
		
        public List<Candidate> GetCandidates()
		{
			List<Candidate> candidates = new List<Candidate>();

			dbAdapter.ExecuteSelectCommand(SqlProcedures.GetCandidates());
			while(dbAdapter.MoveToNextRow())
			{

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
            return Redirect("/Home/FindReminder");
        }
    }
}
