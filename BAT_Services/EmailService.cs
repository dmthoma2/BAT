using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace BAT_Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string from, string fromPW, string SMTPHost, string subject, string body);
    }//IEmailService

    /// <summary>
    /// Contains methods associated with emailing application, trade, and algorithm information.
    /// </summary>
    public class EmailService : IEmailService
    {

        public void SendEmail(string to, string from, string fromPW, string SMTPHost, string subject, string body)
        {
            //TODO TEST EMAIL CLIENT

            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(from, fromPW);
            client.Host = SMTPHost;
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
        }//SendEmail

    }//Email Service
}
