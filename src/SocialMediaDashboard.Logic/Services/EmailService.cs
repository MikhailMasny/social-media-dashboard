using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IEmailService"/>
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        private readonly EmailSettings _emailSettings;

        public EmailService(ILogger<EmailService> logger,
                            IWritableOptions<EmailSettings> emailSettings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (emailSettings is null)
            {
                throw new ArgumentNullException(nameof(emailSettings));
            }

            _emailSettings = emailSettings.Value;
        }

        public async Task SendMessageAsync(string recipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Social Media Dashboard App", _emailSettings.Address));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            try
            {
                using var client = new SmtpClient();

                await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, _emailSettings.UseSsl);
                await client.AuthenticateAsync(_emailSettings.Address, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }
    }
}
