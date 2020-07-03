using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IMediaService"/>
    public class StatisticService : IStatisticService
    {

        private readonly IRepository<Statistic> _statisticRepository;
        private readonly IMediaService _mediaService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IVkService _vkService;

        public StatisticService(IRepository<Statistic> statisticRepository,
                                IMediaService mediaService,
                                ISubscriptionService subscriptionService,
                                IVkService vkService)
        {
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _vkService = vkService ?? throw new ArgumentNullException(nameof(vkService));
        }

        /// <inheritdoc/>
        public async Task AddFollowersFromVk()
        {
            var statistics = new List<Statistic>();
            var subscriptions = await _subscriptionService.GetAllSubscriptionsByType(AccountType.Vk, SubscriptionType.Follower);

            foreach (var subscription in subscriptions)
            {
                var media = await _mediaService.GetAccount(subscription.MediaId);
                var count = await _vkService.GetFollowers(media.AccountName);

                var statistic = new Statistic
                {
                    Count = count.Value,
                    Date = DateTime.Now,
                    SubscriptionId = subscription.Id
                };
                statistics.Add(statistic);
            }

            await _statisticRepository.AddRangeAsync(statistics);
            await _statisticRepository.SaveChangesAsync();
        }
    }
}
