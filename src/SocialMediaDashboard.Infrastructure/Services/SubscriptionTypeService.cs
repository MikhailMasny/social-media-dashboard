using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Exceptions;
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
    /// <inheritdoc cref="ISubscriptionTypeService"/>
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly IRepository<SubscriptionType> _subscriptionTypeRepository;
        private readonly IMapper _mapper;

        public SubscriptionTypeService(IRepository<SubscriptionType> subscriptionTypeRepository,
                                       IMapper mapper)
        {
            _subscriptionTypeRepository = subscriptionTypeRepository ?? throw new ArgumentNullException(nameof(subscriptionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SubscriptionTypeDto>> GetAllAsync()
        {
            var subscriptionTypes = await _subscriptionTypeRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            if (!subscriptionTypes.Any())
            {
                throw new NotFoundException(SubscriptionTypeResource.NotFound);
            }

            return _mapper.Map<List<SubscriptionTypeDto>>(subscriptionTypes);
        }

        public async Task<SubscriptionTypeDto> GetByIdAsync(int id)
        {
            var subscriptionType = await _subscriptionTypeRepository
                .GetEntityWithoutTrackingAsync(subscriptionType => subscriptionType.Id == id);

            if (subscriptionType is null)
            {
                throw new NotFoundException(SubscriptionTypeResource.NotFoundSpecified);
            }

            return _mapper.Map<SubscriptionTypeDto>(subscriptionType);
        }

        public async Task<int> GetByParametersAsync(PlatformType platformType, ObservationType observationType)
        {
            var subscriptionType = await _subscriptionTypeRepository
                .GetEntityAsync(subscriptionType =>
                    subscriptionType.PlatformId == (int)platformType
                    && subscriptionType.ObservationId == (int)observationType);

            return subscriptionType is null
                ? default
                : subscriptionType.Id;
        }

        public async Task<bool> IsExistAsync(int id)
        {
            var subscriptionType = await _subscriptionTypeRepository
                .GetEntityWithoutTrackingAsync(subscriptionType => subscriptionType.Id == id);

            return !(subscriptionType is null);
        }
    }
}
