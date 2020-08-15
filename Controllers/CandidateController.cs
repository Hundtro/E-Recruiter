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
			/*while(dbAdapter.MoveToNextRow())
			{
			    Worker worker = new Worker();
                worker.Id = dbAdapter.GetColumnValue("Id");
                worker.Name = dbAdapter.GetColumnValue("Name");
                worker.Surname = dbAdapter.GetColumnValue("Surname");
				//add other parametes
                workers.Add(worker);
			}*/
			dbAdapter.ClearData();

			return candidates;
        }

        [HttpPost]
        [Route("/InsertCandidate")]
        public IActionResult InsertCandidate(Candidate candidate)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            candidate.Photo.CopyTo(stream);
            byte[] ar = stream.ToArray();
            
            Log.Add(System.Convert.ToBase64String(ar));

            return Redirect("/Home/Index");
        }
    }
}
