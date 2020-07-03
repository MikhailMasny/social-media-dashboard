using Coravel.Invocable;
using SocialMediaDashboard.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Tasks
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
            await _statisticService.AddFollowersFromVk();
        }
    }
}
