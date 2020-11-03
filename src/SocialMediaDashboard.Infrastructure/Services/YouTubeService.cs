using SocialMediaDashboard.Application.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IYouTubeService"/>
    public class YouTubeService : IYouTubeService
    {
        private readonly IRequestService _requestService;

        public YouTubeService(IRequestService requestService)
        {
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        public async Task<int> GetSubscribersByChannelAsync(string channel)
        {
            var statistic = await _requestService.GetDataFromYouTubeApiByChannelAsync(channel);
            var data = statistic.Items;

            return !data.Any()
                ? default
                : int.Parse(
                    data.FirstOrDefault().Statistics.SubscriberCount,
                    CultureInfo.InvariantCulture);
        }
    }
}
