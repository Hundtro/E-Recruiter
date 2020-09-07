using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class HireProcessController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public HireProcessController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }

        public List<HireProcess> GetHireProcesses()
        {
            List<HireProcess> hireProcesses = new List<HireProcess>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetHireProcesses());
            while(dbAdapter.MoveToNextRow())
            {
                HireProcess hireProcess = new HireProcess();

                hireProcess.Id = dbAdapter.GetColumnValue("Id");
                hireProcess.CandidateName = dbAdapter.GetColumnValue("Candidate");
                hireProcess.JobTitleName = dbAdapter.GetColumnValue("JobTitle");
                hireProcess.StepName = dbAdapter.GetColumnValue("Step");
                hireProcess.Comments = dbAdapter.GetColumnValue("Comments");

                hireProcesses.Add(hireProcess);
            }
            dbAdapter.ClearData();

            return hireProcesses;
        }
        
        public HireProcess GetHireProcess(string id)
        {
            HireProcess hireProcess = new HireProcess();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetHireProcess(id));
            while(dbAdapter.MoveToNextRow())
            {
                hireProcess.Id = id;
                hireProcess.CandidateName = dbAdapter.GetColumnValue("Candidate");
                hireProcess.JobTitleName = dbAdapter.GetColumnValue("JobTitle");
                hireProcess.StepName = dbAdapter.GetColumnValue("Step");
                hireProcess.Comments = dbAdapter.GetColumnValue("Comments");
            }
            dbAdapter.ClearData();

            return hireProcess;
        }

        public string GetNextHireStep(string id)
        {
            string stepId;

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetNextHireStepId(id));
            if(dbAdapter.MoveToNextRow())
                stepId = dbAdapter.GetColumnValue("StepId");
            else
                stepId = System.String.Empty;
            dbAdapter.ClearData();

            return stepId;
        }

        [HttpPost]
        [Route("/ViewHireProcess")]
        public IActionResult ViewHireProcess(string hireProcessId)
        {
            ViewData["HireProcess"] = GetHireProcess(hireProcessId);
            ViewData["NextHireStep"] = GetNextHireStep(hireProcessId);
            return View("~/Views/Home/HireProcessView.cshtml");
        }

        [HttpPost]
        [Route("/UpdateHireComments")]
        public IActionResult UpdateHireComments(string hireProcessId, string comments)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.UpdateHireComments(hireProcessId, comments));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/UpdateHireStep")]
        public IActionResult UpdateHireStep(string hireProcessId, string nextStepId)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.UpdateNextHireStep(hireProcessId, nextStepId));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/CloseHireProcess")]
        public IActionResult CloseHireProcess(string id)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.CloseHireProcess(id));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/InsertHireProcess")]
        public IActionResult InsertHireProcess(string candidateId, string jobTitleId)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddHireProcess(candidateId, jobTitleId));

            return Redirect("/Home/ManageRecruitment");
        }

        public List<HireProcess> FindProcess(string candidate, string title)
		{
			List<HireProcess> hireProcesses = new List<HireProcess>();

			dbAdapter.ExecuteSelectCommand(SqlProcedures.FindProcess(candidate, title));
            while(dbAdapter.MoveToNextRow())
            {
                HireProcess hireProcess = new HireProcess();
                
                hireProcess.Id = dbAdapter.GetColumnValue("Id");
                hireProcess.CandidateName = dbAdapter.GetColumnValue("Candidate");
                hireProcess.JobTitleName = dbAdapter.GetColumnValue("Title");

                hireProcesses.Add(hireProcess);
            }
            dbAdapter.ClearData();

            return hireProcesses;
        }
    }
}
