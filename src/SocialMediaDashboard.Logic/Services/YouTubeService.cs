using SocialMediaDashboard.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IYouTubeService"/>
    public class YouTubeService : IYouTubeService
    {
        private readonly IRequestService _requestService;

        public YouTubeService(IRequestService requestService)
        {
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        // TODO: change it to answer + result
        public async Task<string> GetSubscribersByChannelAsync(string channel)
        {
            var statistic = await _requestService.GetDataByChannelFromYouTubeApiAsync(channel);
            var data = statistic.Items;
            if (!data.Any())
            {
                return "0";
            }

            return data.FirstOrDefault().Statistics.SubscriberCount;
        }
    }
}
