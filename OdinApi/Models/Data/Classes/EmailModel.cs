using MimeKit;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public void SendUser(User user, string password)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["SmtpSettings:Username"]));
            email.To.Add(MailboxAddress.Parse(user.mail));
            email.Subject = "Cuenta creada";
            string boddy = @"
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f2f2f2;
                                padding: 20px;
                            }
                            .container {
                                max-width: 500px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                border-radius: 10px;
                                padding: 40px;
                                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                            }
                            h1 {
                                color: #DD6B4D;
                                font-size: 24px;
                                text-align: center;
                                margin-top: 0;
                            }
                            p {
                                font-size: 16px;
                                color: #333333;
                                margin-top: 20px;
                            }
                            .password {
                                font-size: 32px;
                                color: #DD6B4D;
                                text-align: center;
                                margin-top: 20px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <h1>Creacion de cuenta</h1>
                            <p>Estimado/a " + user.name +" "+user.lastName+ @",</p>
                            <p>Su cuenta ha sido creada exitosamente. A continuación, encontrará los detalles de sus credenciales:</p>
                            <p class=""password"">Usuario: " + user.mail+ @"</p>
                            <p class=""password"">Contraseña: " + password+ @"</p>
                        </div>
                    </body>
                    </html>";
            email.Body = new TextPart(TextFormat.Html) { Text = boddy };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_config["SmtpSettings:Server"], int.Parse(_config["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["SmtpSettings:Username"], _config["SmtpSettings:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
