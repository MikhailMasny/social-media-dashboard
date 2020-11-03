using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Exceptions;
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
        private readonly IGenericRepository<Subscription> _subscriptionRepository;
        private readonly IGenericRepository<Statistic> _statisticRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(IGenericRepository<Subscription> subscriptionRepository,
                                   IGenericRepository<Statistic> statisticRepository,
                                   IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _statisticRepository = statisticRepository ?? throw new ArgumentNullException(nameof(statisticRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SubscriptionDto> CreateAsync(string userId, string accountName, int subscriptionTypeId)
        {
            if (!await CanUserCreateSubscriptionAsync(userId, accountName, subscriptionTypeId))
            {
                throw new ConflictException(SubscriptionResource.AlreadyExist);
            }

            var subscription = new Subscription
            {
                UserId = userId,
                AccountName = accountName,
                SubscriptionTypeId = subscriptionTypeId
            };

            await _subscriptionRepository.CreateAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<SubscriptionDto> GetByIdAsync(int id, string userId)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription =>
                    subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                throw new NotFoundException(SubscriptionResource.NotFoundSpecified);
            }

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync(string userId)
        {
            var subscriptions = await _subscriptionRepository
                .GetAllWithoutTracking()
                .Where(subscription => subscription.UserId == userId)
                .ToListAsync();

            if (!subscriptions.Any())
            {
                throw new NotFoundException(SubscriptionResource.NotFound);
            }

            return _mapper.Map<List<SubscriptionDto>>(subscriptions);
        }

        public async Task<SubscriptionDto> UpdateAsync(int id, string userId, string accountName, int subscriptionTypeId)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription =>
                    subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                throw new NotFoundException(SubscriptionResource.NotFoundSpecified);
            }

            static bool CompareAndUpdate(Subscription subscription, string accountName, int subscriptionTypeId)
            {
                bool toUpdate = false;

                if (subscription.AccountName != accountName)
                {
                    subscription.AccountName = accountName;
                    toUpdate = true;
                }

                if (subscription.SubscriptionTypeId != subscriptionTypeId)
                {
                    subscription.SubscriptionTypeId = subscriptionTypeId;
                    toUpdate = true;
                }

                return toUpdate;
            }

            if (!CompareAndUpdate(subscription, accountName, subscriptionTypeId))
            {
                throw new ConflictException(SubscriptionResource.SameData);
            }

            _subscriptionRepository.Update(subscription);

            var statistics = await _statisticRepository
                .GetAllWithoutTracking()
                .Where(statistic => statistic.SubscriptionId == subscription.Id)
                .ToListAsync();

            _statisticRepository.DeleteRange(statistics);
            await _subscriptionRepository.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task DeleteByIdAsync(int id, string userId)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription =>
                    subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                throw new NotFoundException(SubscriptionResource.NotFoundSpecified);
            }

            _subscriptionRepository.Delete(subscription);
            await _subscriptionRepository.SaveChangesAsync();
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
                .GetEntityWithoutTrackingAsync(subscriptionType =>
                    subscriptionType.UserId == userId
                    && subscriptionType.AccountName == accountName
                    && subscriptionType.SubscriptionTypeId == subscriptionTypeId);

            return selectedSubscription is null;
        }
    }
}
