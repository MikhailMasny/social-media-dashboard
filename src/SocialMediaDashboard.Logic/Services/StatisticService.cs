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
        private readonly IVkService _vkService;

        public StatisticService(IRepository<Statistic> statisticRepository, IMediaService mediaService, IVkService vkService)
        {
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
            _vkService = vkService ?? throw new ArgumentNullException(nameof(vkService));
        }

        /// <inheritdoc/>
        public async Task AddDataByVkAccounts()
        {
            var statistics = new List<Statistic>();
            var accounts = await _mediaService.GetAllAccounts();
            foreach (var account in accounts)
            {
                var count = await _vkService.GetFollowers(account.AccountName);

                var statistic = new Statistic
                {
                    Type = 1,
                    Count = count.Value,
                    Date = DateTime.Now,
                    MediaId = account.Id
                };

                statistics.Add(statistic);
            }

            await _statisticRepository.AddRangeAsync(statistics);
            await _statisticRepository.SaveChangesAsync();
        }
    }
}
