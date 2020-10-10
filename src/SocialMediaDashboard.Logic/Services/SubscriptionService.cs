using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="ISubscriptionService"/>
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<Statistic> _statisticRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(IRepository<Subscription> subscriptionRepository,
                                   IRepository<Statistic> statisticRepository,
                                   IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> CreateAsync(string userId, string accountName, int subscriptionTypeId)
        {
            SubscriptionResult subscriptionResult;

            var canUserCreateSubscription = await CanUserCreateSubscriptionAsync(userId, accountName, subscriptionTypeId);
            if (!canUserCreateSubscription)
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.AlreadyExist,
                };

                return (new SubscriptionDto(), subscriptionResult);
            }

            var subscription = new Subscription
            {
                UserId = userId,
                AccountName = accountName,
                SubscriptionTypeId = subscriptionTypeId
            };

            await _subscriptionRepository.CreateAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            subscriptionResult = new SubscriptionResult
            {
                Result = true,
                Message = SubscriptionResource.Added,
            };

            return (subscriptionDto, subscriptionResult);
        }

        public async Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> GetByIdAsync(int id, string userId)
        {
            SubscriptionResult subscriptionResult;

            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound,
                };

                return (new SubscriptionDto(), subscriptionResult);
            }

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            subscriptionResult = new SubscriptionResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionDto, subscriptionResult);
        }

        public async Task<(IEnumerable<SubscriptionDto> subscriptionDto, SubscriptionResult subscriptionResult)> GetAllAsync(string userId)
        {
            SubscriptionResult subscriptionResult;

            var subscriptions = await _subscriptionRepository
                .GetAllWithoutTracking()
                .Where(subscription => subscription.UserId == userId)
                .ToListAsync();

            if (!subscriptions.Any())
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound,
                };

                return (new List<SubscriptionDto>(), subscriptionResult);
            }

            var subscriptionDtos = _mapper.Map<List<SubscriptionDto>>(subscriptions);
            subscriptionResult = new SubscriptionResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionDtos, subscriptionResult);
        }

        public async Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> UpdateAsync(int id, string userId, string accountName, int subscriptionTypeId)
        {
            SubscriptionResult subscriptionResult;

            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound,
                };

                return (new SubscriptionDto(), subscriptionResult);
            }

            if (subscription.AccountName == accountName && subscription.SubscriptionTypeId == subscriptionTypeId)
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.SameData,
                };

                return (new SubscriptionDto(), subscriptionResult);
            }

            subscription.AccountName = accountName;
            subscription.SubscriptionTypeId = subscriptionTypeId;
            _subscriptionRepository.Update(subscription);

            var statistics = await _statisticRepository
                .GetAllWithoutTracking()
                .Where(statistic => statistic.SubscriptionId == subscription.Id)
                .ToListAsync();

            _statisticRepository.DeleteRange(statistics);
            await _subscriptionRepository.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            subscriptionResult = new SubscriptionResult
            {
                Result = true,
                Message = SubscriptionResource.Updated,
            };

            return (subscriptionDto, subscriptionResult);
        }

        public async Task<SubscriptionResult> DeleteByIdAsync(int id, string userId)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                return new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound,
                };
            }

            _subscriptionRepository.Delete(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return new SubscriptionResult
            {
                Result = true,
                Message = SubscriptionResource.Deleted,
            };
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAccountNamesBySubscriptionTypeIdAsync(int subscriptionTypeId)
        {

            var subscriptions = await _subscriptionRepository
                .GetAllWithoutTracking()
                .Where(subscription => subscription.SubscriptionTypeId == subscriptionTypeId)
                .ToListAsync();

            return _mapper.Map<List<SubscriptionDto>>(subscriptions);
        }

        private async Task<bool> CanUserCreateSubscriptionAsync(string userId, string accountName, int subscriptionTypeId)
        {
            var selectedSubscription = await _subscriptionRepository
                .GetEntityWithoutTrackingAsync(subscriptionType => subscriptionType.UserId == userId
                    && subscriptionType.AccountName == accountName
                    && subscriptionType.SubscriptionTypeId == subscriptionTypeId);

            return selectedSubscription is null;
        }
    }
}
