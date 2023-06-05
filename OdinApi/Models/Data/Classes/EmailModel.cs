using MimeKit;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;


namespace OdinApi.Models.Data.Classes
{
    public class EmailModel : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailModel(IConfiguration config)
        {
            _config = config;
        }


        public void SendEmail(Email request)
        {


            var email = new MimeMessage();


            email.From.Add(MailboxAddress.Parse(_config["SmtpSettings:Username"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_config["SmtpSettings:Server"], int.Parse(_config["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["SmtpSettings:Username"], _config["SmtpSettings:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);

        }

    }
}
