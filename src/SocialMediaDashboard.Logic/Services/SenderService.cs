using SocialMediaDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="ISenderService"/>
    public class SenderService : ISenderService
    {
        private readonly IMailService _mailService;
        private readonly IRenderService _renderService;

        public SenderService(IMailService mailService,
                             IRenderService renderService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _renderService = renderService ?? throw new ArgumentNullException(nameof(renderService));
        }

        public async Task RenderAndSendAsync<TModel>(TModel model, string view, string recipient, string subject)
        {
            var body = await _renderService.RenderViewToStringAsync(view, model);
            await _mailService.SendMessageAsync(recipient, subject, body);
        }
    }
}
