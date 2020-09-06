using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace erecruiter
{
    public class EmailTemplateController : Controller
    {
        private IConfiguration configuration;
        private DatabaseAdapter dbAdapter;

        public EmailTemplateController(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbAdapter = new DatabaseAdapter(configuration["database"]);
        }

        public List<EmailTemplate> GetEmailTemplates()
        {
            List<EmailTemplate> emailTemplates = new List<EmailTemplate>();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetEmailTemplates());
            while(dbAdapter.MoveToNextRow())
            {
                EmailTemplate emailTemplate = new EmailTemplate();
                
                emailTemplate.Id = dbAdapter.GetColumnValue("Id");
                emailTemplate.Title = dbAdapter.GetColumnValue("Title");
                emailTemplate.Message = dbAdapter.GetColumnValue("Message");

                emailTemplates.Add(emailTemplate);
            }
            dbAdapter.ClearData();

            return emailTemplates;
        }

        [HttpPost]
        [Route("/InsertEmailTemplate")]
        public IActionResult InsertEmailTemplate(EmailTemplate emailTemplate)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.AddEmailTemplate(emailTemplate));

            return Redirect("/Home/ContactCandidate");
        }

        public EmailTemplate GetEmailTemplate(string id)
        {
            EmailTemplate emailTemplate = new EmailTemplate();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetEmailTemplate(id));
            while(dbAdapter.MoveToNextRow())
            {
                emailTemplate.Id = dbAdapter.GetColumnValue("Id");
                emailTemplate.Title = dbAdapter.GetColumnValue("Title");
                emailTemplate.Message = dbAdapter.GetColumnValue("Message");
            }
            dbAdapter.ClearData();

            return emailTemplate;
        }

        [HttpPost]
        [Route("/UpdateEmailTemplate")]
        public IActionResult UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            dbAdapter.ExecuteCommand(SqlProcedures.UpdateEmailTemplate(emailTemplate));

            return Redirect("/Home/ContactCandidate");
        }

        [HttpPost]
        [Route("/SelectEmailTemplate")]
        public IActionResult SelectEmailTemplate(string id)
        {
            EmailTemplate emailTemplate = new EmailTemplate();

            dbAdapter.ExecuteSelectCommand(SqlProcedures.GetEmailTemplate(id));
            while(dbAdapter.MoveToNextRow())
            {
                emailTemplate.Title = dbAdapter.GetColumnValue("Title");
                emailTemplate.Message = dbAdapter.GetColumnValue("Message");
            }
            dbAdapter.ClearData();

            ViewData["DefaultTitle"] = emailTemplate.Title;
            ViewData["DefaultText"] = emailTemplate.Message;
            ViewData["EmailTemplates"] = new EmailTemplateController(this.configuration).GetEmailTemplates();
            return View("~/Views/Home/ContactCandidate.cshtml");
        }
    }
}
