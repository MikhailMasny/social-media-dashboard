using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<StatisticService> _logger;

        public StatisticService(IRepository<Statistic> statisticRepository,
                                IMediaService mediaService,
                                ISubscriptionService subscriptionService,
                                IVkService vkService,
                                ILogger<StatisticService> logger)
        {
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _vkService = vkService ?? throw new ArgumentNullException(nameof(vkService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task AddFollowersFromVkAsync()
        {
            var statistics = new List<Statistic>();
            var subscriptions = await _subscriptionService.GetAllSubscriptionsByTypeAsync(AccountType.Vk, SubscriptionType.Follower);

            if (subscriptions.Any())
            {
                foreach (var subscription in subscriptions)
                {
                    var media = await _mediaService.GetAccountAsync(subscription.MediaId);
                    int? count;

                    try
                    {
                        count = await _vkService.GetFollowersAsync(media.AccountName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }

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
}
