using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace erecruiter
{
    public class WorkerController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public WorkerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }
		
        public List<Worker> GetWorkers()
		{
			List<Worker> workers = new List<Worker>();

			dbAdapter.ExecuteSelectCommand(SqlProcedures.SelectWorkers());
			while(dbAdapter.MoveToNextRow())
			{
			    Worker worker = new Worker();
                worker.Id = dbAdapter.GetColumnValue("Id");
                worker.Name = dbAdapter.GetColumnValue("Name");
                worker.Surname = dbAdapter.GetColumnValue("Surname");
				//add other parametes
                workers.Add(worker);
			}
			dbAdapter.ClearData();

			return workers;
        }
    }
}
