using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Enums;
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
        private readonly IMapper _mapper;

        public SubscriptionService(IRepository<Subscription> subscriptionRepository,
                                   IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> CreateSubscriptionAsync(string userId, string accountName, int subscriptionTypeId)
        {
            SubscriptionResult subscriptionResult;

            var canUserCreateSubscription = await CanUserCreateSubscriptionAsync(userId, accountName, subscriptionTypeId);
            if (!canUserCreateSubscription)
            {
                subscriptionResult = new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.AlreadyExist
                };

                return (null, subscriptionResult);
            }

            var subscription = new Subscription
            {
                UserId = userId,
                Name = accountName, // TODO: change to AccountName
                SubscriptionTypeId = subscriptionTypeId
            };

            await _subscriptionRepository.CreateAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            subscriptionResult = new SubscriptionResult
            {
                Result = true,
                Message = SubscriptionResource.Added
            };

            return (subscriptionDto, subscriptionResult);
        }

        public async Task<SubscriptionResult> DeleteSubscriptionByIdAsync(string userId, int id)
        {
            var subscription = await _subscriptionRepository
                .GetEntityAsync(subscription => subscription.Id == id && subscription.UserId == userId);

            if (subscription is null)
            {
                return new SubscriptionResult
                {
                    Result = false,
                    Message = SubscriptionResource.NotFound
                };
            }

            _subscriptionRepository.Delete(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return new SubscriptionResult
            {
                Result = true,
                Message = SubscriptionResource.Deleted
            };
        }

        private async Task<bool> CanUserInteractWithSubscriptionAsync(string userId, int id)
        {
            var selectedSubscription = await _subscriptionRepository
                .GetEntityWithoutTrackingAsync(subscription => subscription.UserId == userId && subscription.Id == id);

            return !(selectedSubscription is null);
        }

        private async Task<bool> CanUserCreateSubscriptionAsync(string userId, string accountName, int subscriptionTypeId)
        {
            var selectedSubscription = await _subscriptionRepository
                .GetEntityWithoutTrackingAsync(subscriptionType => subscriptionType.UserId == userId
                    && subscriptionType.Name == accountName
                    && subscriptionType.SubscriptionTypeId == subscriptionTypeId);

            return selectedSubscription is null;
        }





        //public async Task<bool> AddSubscriptionAsync(string userId, int accountId, string account, AccountKind accountType, SubscriptionKind subscriptionType)
        //{
        //    var canCreateSubscription = await CanUserCreateSubscription(userId, account, accountType, subscriptionType);
        //    if (!canCreateSubscription)
        //    {
        //        return false;
        //    }

        //    var subscription = new Subscription
        //    {
        //        Type = subscriptionType,
        //        IsDisplayed = true,
        //        AccountId = accountId
        //    };

        //    await _subscriptionRepository.AddAsync(subscription);
        //    await _subscriptionRepository.SaveChangesAsync();

        //    return true;
        //}

        //public async Task<IEnumerable<SubscriptionDto>> GetAllUserSubscriptionsAsync(string userId)
        //{
        //    var subscriptions = await _subscriptionRepository.GetAllWithoutTracking()
        //        .Include(s => s.Account)
        //        .Where(s => s.Account.UserId == userId)
        //        .ToListAsync();

        //    var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

        //    return subscriptionsDto;
        //}

        //public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByTypeAsync(AccountKind accountType, SubscriptionKind subscriptionType)
        //{
        //    var subscriptions = await _subscriptionRepository.GetAllWithoutTracking()
        //        .Include(s => s.Account)
        //        .Where(s => s.Type == subscriptionType && s.Account.Type == accountType)
        //        .ToListAsync();

        //    var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

        //    return subscriptionsDto;
        //}

        //public async Task<bool> DeleteSubscriptionAsync(int id, string userId)
        //{
        //    var subscription = await _subscriptionRepository.GetAll()
        //        .Include(s => s.Account)
        //        .FirstOrDefaultAsync(s => s.Account.UserId == userId && s.Id == id);

        //    //var subscription = await _subscriptionRepository
        //    if (subscription == null)
        //    {
        //        return false;
        //    }

        //    _subscriptionRepository.Delete(subscription);
        //    await _subscriptionRepository.SaveChangesAsync();

        //    return true;
        //}

        //public async Task<bool> SubscriptionExistAsync(int id)
        //{
        //    var subscription = await _subscriptionRepository.GetEntityAsync(m => m.Id == id);
        //    if (subscription == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private async Task<bool> CanUserCreateSubscription(string userId, string account, AccountKind accountType, SubscriptionKind subscriptionType)
        //{
        //    var selectedSubscriptions = await _subscriptionRepository.GetAllWithoutTracking()
        //        .Include(s => s.Account)
        //        .Where(s => s.Account.UserId == userId && s.Account.Name == account && s.Account.Type == accountType)
        //        .ToListAsync();

        //    foreach (var subscription in selectedSubscriptions)
        //    {
        //        if (subscription.Type == subscriptionType)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
