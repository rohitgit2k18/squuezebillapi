using Services.DtoServiceModel.DtoServiceRequetModel;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Services
{
    public class EmailService
    {
        public string SendEmail(SendEmailRequestModel SERM)
        {
            string Result = "";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("noreply@squeezebill.com");
                mail.To.Add(SERM.ToEmailId);
                mail.Subject = SERM.Subject;
                mail.Body = SERM.MessageBody;              
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("testifiedemail@gmail.com", "testifiedpassword@hackfree");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                Result = "Ok";
            }
            catch (Exception ex)
            {
                Result = Convert.ToString(ex);
            }
            return Result;
        }
    }
}
