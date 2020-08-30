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
                hireStep.Order = dbAdapter.GetColumnValue("Order");
                hireStep.Name = dbAdapter.GetColumnValue("Name");
                hireStep.Id = dbAdapter.GetColumnValue("Description");

                hireSteps.Add(hireStep);
            }
            dbAdapter.ClearData();

            return hireSteps;
        }
    }
}
