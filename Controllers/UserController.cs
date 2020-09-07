using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class UserController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }
		
        public List<User> GetUsers()
		{
            List<User> users = new List<User>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetUsers());
            while(dbAdapter.MoveToNextRow())
            {
                User user = new User();
                
                user.Id = dbAdapter.GetColumnValue("Id");
                user.Login = dbAdapter.GetColumnValue("Login");
                user.Password = dbAdapter.GetColumnValue("Password");
                user.FullName = dbAdapter.GetColumnValue("FullName");
                user.CanEdit = dbAdapter.GetColumnValue("CanEdit");
                user.CanConfig = dbAdapter.GetColumnValue("CanConfig");

                users.Add(user);
            }
            dbAdapter.ClearData();

            return users;
        }

        public User GetUserById(string id)
        {
            User user = new User();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetUserById(id));
            while(dbAdapter.MoveToNextRow())
            {
                user.Id = dbAdapter.GetColumnValue("Id");
                user.Login = dbAdapter.GetColumnValue("Login");
                user.Password = dbAdapter.GetColumnValue("Password");
                user.FullName = dbAdapter.GetColumnValue("FullName");
                user.CanEdit = dbAdapter.GetColumnValue("CanEdit");
                user.CanConfig = dbAdapter.GetColumnValue("CanConfig");
            }
            dbAdapter.ClearData();

            return user;
        }

        [HttpPost]
        [Route("/InsertUser")]
        public IActionResult InsertUser(User user)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddUser(user));

            return Redirect("/Home/Configuration");
        }

        [HttpPost]
        [Route("/ViewUser")]
        public IActionResult ViewUser(string id)
        {
            User user = GetUserById(id); 

            ViewData["User"] = user;
            return View("~/Views/Home/UserView.cshtml");
        }

        [HttpPost]
        [Route("/UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.UpdateUser(user));

            return Redirect("/Home/Configuration");
        }

        [HttpPost]
        [Route("/DeleteUser")]
        public IActionResult DeleteUser(string id)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.DeleteUser(id));

            return Redirect("/Home/Configuration");
        }
    }
}
	
