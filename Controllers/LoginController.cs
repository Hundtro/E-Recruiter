using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace erecruiter
{
    public class LoginController : Controller 
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }
        
        public bool Login()
        {
            bool result = false;
            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetUserByLogin(Session.Login, Session.Password));
            
            if(dbAdapter.MoveToNextRow()) 
            {
                Session.UserId = dbAdapter.GetColumnValue("Id");
                Session.FullName = dbAdapter.GetColumnValue("FullName"); 
                Session.CanEdit = int.Parse(dbAdapter.GetColumnValue("CanEdit"));
                Session.CanConfig = int.Parse(dbAdapter.GetColumnValue("CanConfig"));
                result = true; 
            }
            
            dbAdapter.ClearData();
            return result;
        }
    }
}
