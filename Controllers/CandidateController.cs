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
                candidate.PhotoData = dbAdapter.GetColumnValue("Photo");

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

            return Redirect("/Home/FindCandidate");
        }

        [HttpPost]
        [Route("/FindCandidate")]
        public IActionResult FindCandiddate(string name, string surname)
        {
            ViewData["Candidates"] = new CandidateController(this.configuration).GetCandidates(name, surname);
            return View("~/Views/Home/FindCandidate.cshtml");
        }

        [HttpPost]
        [Route("/ViewCandidate")]
        public IActionResult ViewCandidate(string id)
        {
            Candidate candidate = new Candidate();
            
            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetCandidate(id));
			while(dbAdapter.MoveToNextRow())
			{
                candidate.Id = id;
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
            }
            dbAdapter.ClearData();

            List<Experience> experiences = new ExperienceController(this.configuration).GetExperiences(id);

            ViewData["Experiences"] = experiences; 
            ViewData["Candidate"] = candidate;
            return View("~/Views/Home/CandidateView.cshtml");
        }
    }
}
