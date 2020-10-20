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

        public async Task<(IEnumerable<SubscriptionTypeDto> subscriptionTypeDtos, OperationResult operationResult)> GetAllAsync()
        {
            OperationResult operationResult;

            var subscriptionTypes = await _subscriptionTypeRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            if (!subscriptionTypes.Any())
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionTypeResource.NotFound,
                };

                return (new List<SubscriptionTypeDto>(), operationResult);
            }

            var subscriptionTypeDtos = _mapper.Map<List<SubscriptionTypeDto>>(subscriptionTypes);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionTypeDtos, operationResult);
        }

        public async Task<(SubscriptionTypeDto subscriptionTypeDto, OperationResult operationResult)> GetByIdAsync(int id)
        {
            OperationResult operationResult;

            var subscriptionType = await _subscriptionTypeRepository
                .GetEntityWithoutTrackingAsync(subscriptionType => subscriptionType.Id == id);

            if (subscriptionType is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = SubscriptionTypeResource.NotFoundSpecified,
                };

                return (new SubscriptionTypeDto(), operationResult);
            }

            var subscriptionTypeDto = _mapper.Map<SubscriptionTypeDto>(subscriptionType);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (subscriptionTypeDto, operationResult);
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
