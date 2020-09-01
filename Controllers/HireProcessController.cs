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
                hireProcess.Id = dbAdapter.GetColumnValue("Id");
                hireProcess.CandidateName = dbAdapter.GetColumnValue("Candidate");
                hireProcess.JobTitleName = dbAdapter.GetColumnValue("JobTitle");
                hireProcess.StepName = dbAdapter.GetColumnValue("Step");
                hireProcess.Comments = dbAdapter.GetColumnValue("Comments");
            }
            dbAdapter.ClearData();

            return hireProcess;
        }

        [HttpPost]
        [Route("/ViewHireProcess")]
        public IActionResult ViewHireProcess(string hireProcessId)
        {
            ViewData["HireProcess"] = GetHireProcess(hireProcessId);
            ViewData["NextHireStep"] = "1234";
            return View("~/Views/Home/HireProcessView.cshtml");
        }
    }
}
