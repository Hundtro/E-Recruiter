using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class JobTitleController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public JobTitleController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }

        public List<JobTitle> GetJobTitles()
        {
            List<JobTitle> jobTitles = new List<JobTitle>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetJobTitles());
            while(dbAdapter.MoveToNextRow())
            {
                JobTitle jobTitle = new JobTitle();

                jobTitle.Id = dbAdapter.GetColumnValue("Id");
                jobTitle.Title = dbAdapter.GetColumnValue("Title");

                jobTitles.Add(jobTitle);
            }
            dbAdapter.ClearData();

            return jobTitles;
        }

        public JobTitle GetJobTitle(string jobTitleId)
        {
            JobTitle jobTitle = new JobTitle();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetJobTitle(jobTitleId));
            while(dbAdapter.MoveToNextRow())
            {
                jobTitle.Id = jobTitleId;
                jobTitle.Title = dbAdapter.GetColumnValue("Title");
                jobTitle.DefaultSalary = dbAdapter.GetColumnValue("DefaultSalary");
                jobTitle.Description = dbAdapter.GetColumnValue("Description");
                jobTitle.CreatedBy = dbAdapter.GetColumnValue("CreatedBy");
            }
            dbAdapter.ClearData();

            return jobTitle;
        }

        [HttpPost]
        [Route("/InsertJobTitle")]
        public IActionResult InsertJobTitle(JobTitle jobTitle)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddJobTitle(jobTitle));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/ViewJobTitle")]
        public IActionResult ViewJobTitle(string jobTitleId)
        {
            JobTitle jobTitle = GetJobTitle(jobTitleId); 
            List<HireStep> hireSteps = new HireStepController(this.configuration).GetHireSteps(jobTitleId);

            ViewData["JobTitle"] = jobTitle; 
            ViewData["HireSteps"] = hireSteps;
            return View("~/Views/Home/JobTitleView.cshtml");
        }

        [HttpPost]
        [Route("/UpdateJobTitle")]
        public IActionResult UpdateJobTitle(JobTitle jobTitle)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.UpdateJobTitle(jobTitle));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/DeleteJobTitle")]
        public IActionResult DeleteJobTitle(string id)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.DeleteJobTitle(id));

            return Redirect("/Home/ManageRecruitment");
        }
    }
}
