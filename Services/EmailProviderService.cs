using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Models.Domain.Custom;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmailProviderService
    {
        Task Send(MailRequest request);
    }

    public class EmailProviderService: IEmailProviderService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailProviderService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task Send(MailRequest request)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", request.Email));
                message.Subject = request.Subject;
                message.Body = new TextPart("html") { Text = request.Body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port);
                    await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
