using Coravel.Invocable;
using SocialMediaDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Tasks
{
    /// <summary>
    /// Statistic task for Coravel.
    /// </summary>
    public class StatisticInvocable : IInvocable
    {
        private readonly IStatisticService _statisticService;

        public StatisticInvocable(IStatisticService statisticService)
        {
            _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
        }

        /// <inheritdoc/>
        public async Task Invoke()
        {
            await _statisticService.GetFollowersFromVkAsync();
            await _statisticService.GetFollowersFromInstagramAsync();
            await _statisticService.GetSubscribersFromYouTubeAsync();
        }
    }
}
