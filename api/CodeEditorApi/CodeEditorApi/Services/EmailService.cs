using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace CodeEditorApi.Services
{
    public class MailRequest
    { 
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }


    public class EmailService : IEmailService
    {

        private readonly string _host;
        private readonly string _mail;
        private readonly string _displayName;
        private readonly string _password;
        private readonly int _port;

        public EmailService(IConfiguration configuration)
        {
            _host = configuration["Email:Host"];
            _mail = configuration["Email:Mail"];
            _displayName = configuration["Email:DisplayName"];
            _password = configuration["Email:Password"];
            _port = int.Parse(configuration["Email:Port"]);
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            MailboxAddress from = MailboxAddress.Parse(_mail);
            from.Name = _displayName;
            email.From.Add(from);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mail, _password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
