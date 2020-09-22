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
    /// <inheritdoc cref="IAccountService"/>
    public class StatisticService : IStatisticService
    {

        private readonly IRepository<Statistic> _statisticRepository;
        private readonly IAccountService _accountService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IVkService _vkService;
        private readonly ILogger<StatisticService> _logger;

        public StatisticService(IRepository<Statistic> statisticRepository,
                                IAccountService accountService,
                                ISubscriptionService subscriptionService,
                                IVkService vkService,
                                ILogger<StatisticService> logger)
        {
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
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
                    var account = await _accountService.GetAccountAsync(subscription.AccountId);
                    int? count;

                    try
                    {
                        count = await _vkService.GetFollowersByUserNameAsync(account.Name);
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
