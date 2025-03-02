using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			try
			{

                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse(mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using SmtpClient smtp = new();
                await smtp.ConnectAsync(mailSettings.SmtpHost, mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailSettings.SmtpUser, mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
			}
			catch (Exception)
			{

				
			}
        }
    }
}
