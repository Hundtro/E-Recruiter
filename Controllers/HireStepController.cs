using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class HireStepController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public HireStepController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }

        public List<HireStep> GetHireSteps(string jobTitleId)
        {
            List<HireStep> hireSteps = new List<HireStep>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetHireSteps(jobTitleId));
            while(dbAdapter.MoveToNextRow())
            {
                HireStep hireStep = new HireStep();

                hireStep.Id = dbAdapter.GetColumnValue("Id");
                hireStep.JobTitleId = jobTitleId;
                hireStep.OrderNo = dbAdapter.GetColumnValue("OrderNo");
                hireStep.Name = dbAdapter.GetColumnValue("Name");
                hireStep.Description = dbAdapter.GetColumnValue("Description");

                hireSteps.Add(hireStep);
            }
            dbAdapter.ClearData();

            return hireSteps;
        }

        [HttpPost]
        [Route("/InsertHireStep")]
        public IActionResult InsertHireStep(HireStep hireStep)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddHireStep(hireStep));

            return Redirect("/Home/ManageRecruitment");
        }

        [HttpPost]
        [Route("/DeleteHireStep")]
        public IActionResult DeleteHireStep(string id)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.DeleteHireStep(id));

            return Redirect("/Home/ManageRecruitment");
        }
    }
}
