using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System;

namespace erecruiter
{
    public class EmailSender 
    {
        public static void Send(string subject, string text, string adress, IConfiguration configuration)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(configuration["emailAdress"]);
            message.To.Add(new MailAddress(adress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = text;
 
            smtp.Port = Convert.ToInt32(configuration["smtpPort"]);
            smtp.Host = configuration["smtpHost"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(configuration["emailAdress"], configuration["emailPassword"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
 
            smtp.Send(message);
        }
    }
}
