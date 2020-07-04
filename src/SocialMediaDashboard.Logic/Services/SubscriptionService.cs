using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
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

        /// <inheritdoc/>
        public async Task<bool> AddSubscriptionAsync(string userId, int mediaId, string account, AccountType accountType, SubscriptionType subscriptionType)
        {
            var canCreateSubscription = await CanUserCreateSubscription(userId, account, accountType, subscriptionType);
            if (!canCreateSubscription)
            {
                return false;
            }

            var subscription = new Subscription
            {
                Type = subscriptionType,
                IsDisplayed = true,
                MediaId = mediaId
            };

            await _subscriptionRepository.AddAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SubscriptionDto>> GetAllUserSubscriptionsAsync(string userId)
        {
            var subscriptions = await _subscriptionRepository.GetAllWithoutTracking()
                .Include(s => s.Media)
                .Where(s => s.Media.UserId == userId)
                .ToListAsync();

            var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return subscriptionsDto;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByTypeAsync(AccountType accountType, SubscriptionType subscriptionType)
        {
            var subscriptions = await _subscriptionRepository.GetAllWithoutTracking()
                .Include(s => s.Media)
                .Where(s => s.Type == subscriptionType && s.Media.Type == accountType)
                .ToListAsync();

            var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return subscriptionsDto;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteSubscriptionAsync(int id, string userId)
        {
            var subscription = await _subscriptionRepository.GetAll()
                .Include(s => s.Media)
                .FirstOrDefaultAsync(s => s.Media.UserId == userId && s.Id == id);


            //var subscription = await _subscriptionRepository
            if (subscription == null)
            {
                return false;
            }

            _subscriptionRepository.Delete(subscription);
            await _subscriptionRepository.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> SubscriptionExistAsync(int id)
        {
            var subscription = await _subscriptionRepository.GetEntityAsync(m => m.Id == id);
            if (subscription == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CanUserCreateSubscription(string userId, string account, AccountType accountType, SubscriptionType subscriptionType)
        {
            var selectedSubscriptions = await _subscriptionRepository.GetAllWithoutTracking()
                .Include(s => s.Media)
                .Where(s => s.Media.UserId == userId && s.Media.AccountName == account && s.Media.Type == accountType)
                .ToListAsync();

            foreach (var subscription in selectedSubscriptions)
            {
                if (subscription.Type == subscriptionType)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
