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
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByType(AccountType accountType, SubscriptionType subscriptionType)
        {
            var subscriptions = await _subscriptionRepository.GetAll()
                .Include(s => s.Media)
                .Where(s => s.Type == subscriptionType && s.Media.Type == accountType)
                .ToListAsync();

            var subscriptionsDto = _mapper.Map<List<SubscriptionDto>>(subscriptions);

            return subscriptionsDto;
        }
    }
}
