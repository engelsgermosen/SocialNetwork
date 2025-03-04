using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Domain.Settings;

namespace SocialNetwork.Infraestructure.Shared.Service
{
    public class EmailService : IEmailService
    {

		private MailSettings mailSettings;

        public EmailService(IOptions<MailSettings> options)
        {
            mailSettings = options.Value;
        }

        public async Task SendAsync(EmailRequest request)
        {		
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            BodyBuilder builder = new();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using (SmtpClient smtp = new())
            {
                smtp.Connect(mailSettings.SmtpHost, mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.SmtpUser, mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            };
        }
    }
}
