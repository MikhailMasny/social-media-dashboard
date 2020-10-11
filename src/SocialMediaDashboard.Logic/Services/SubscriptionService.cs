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

        public async Task<(SubscriptionDto subscriptionDto, OperationResult operationResult)> CreateAsync(string userId, string accountName, int subscriptionTypeId)
        {
            OperationResult operationResult;

            var canUserCreateSubscription = await CanUserCreateSubscriptionAsync(userId, accountName, subscriptionTypeId);
            if (!canUserCreateSubscription)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.AlreadyExist,
                };

                return (new SubscriptionDto(), operationResult);
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
            operationResult = new OperationResult
            {
                Result = true,
                Message = SubscriptionResource.Added,
            };

            return (subscriptionDto, operationResult);
        }

        public async Task<(SubscriptionDto subscriptionDto, OperationResult operationResult)> GetByIdAsync(int id, string userId)
        {
            OperationResult operationResult;

            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFoundSpecified,
                };

                return (new SubscriptionDto(), operationResult);
            }

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionDto, operationResult);
        }

        public async Task<(IEnumerable<SubscriptionDto> subscriptionDto, OperationResult operationResult)> GetAllAsync(string userId)
        {
            OperationResult operationResult;

            var subscriptions = await _subscriptionRepository
                .GetAllWithoutTracking()
                .Where(subscription => subscription.UserId == userId)
                .ToListAsync();

            if (!subscriptions.Any())
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound,
                };

                return (new List<SubscriptionDto>(), operationResult);
            }

            var subscriptionDtos = _mapper.Map<List<SubscriptionDto>>(subscriptions);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionDtos, operationResult);
        }

        public async Task<(SubscriptionDto subscriptionDto, OperationResult operationResult)> UpdateAsync(int id, string userId, string accountName, int subscriptionTypeId)
        {
            OperationResult operationResult;

            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFoundSpecified,
                };

                return (new SubscriptionDto(), operationResult);
            }

            static bool CompareAndUpdate(Subscription subscription, string accountName, int subscriptionTypeId)
            {
                bool update = false;

                if (subscription.AccountName != accountName)
                {
                    subscription.AccountName = accountName;
                    update = true;
                }

                if (subscription.SubscriptionTypeId != subscriptionTypeId)
                {
                    subscription.SubscriptionTypeId = subscriptionTypeId;
                    update = true;
                }

                return update;
            }

            if (!CompareAndUpdate(subscription, accountName, subscriptionTypeId))
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.SameData,
                };

                return (new SubscriptionDto(), operationResult);
            }

            _subscriptionRepository.Update(subscription);

            var statistics = await _statisticRepository
                .GetAllWithoutTracking()
                .Where(statistic => statistic.SubscriptionId == subscription.Id)
                .ToListAsync();

            _statisticRepository.DeleteRange(statistics);
            await _subscriptionRepository.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            operationResult = new OperationResult
            {
                Result = true,
                Message = SubscriptionResource.Updated,
            };

            return (subscriptionDto, operationResult);
        }

        public async Task<OperationResult> DeleteByIdAsync(int id, string userId)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                return new OperationResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFoundSpecified,
                };
            }

            _subscriptionRepository.Delete(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return new OperationResult
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
