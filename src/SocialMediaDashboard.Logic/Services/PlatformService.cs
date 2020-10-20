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
    /// <inheritdoc cref="IPlatformService"/>
    public class PlatformService : IPlatformService
    {
        private readonly IRepository<Platform> _platformRepository;
        private readonly IMapper _mapper;

        public PlatformService(IRepository<Platform> platformRepository,
                               IMapper mapper)
        {
            _platformRepository = platformRepository ?? throw new ArgumentNullException(nameof(platformRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<(IEnumerable<PlatformDto> platformDtos, OperationResult operationResult)> GetAllAsync()
        {
            OperationResult operationResult;

            var platforms = await _platformRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            if (!platforms.Any())
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = PlatformResource.NotFound,
                };

                return (new List<PlatformDto>(), operationResult);
            }

            var platformDtos = _mapper.Map<List<PlatformDto>>(platforms);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (platformDtos, operationResult);
        }

        public async Task<(PlatformDto platformDto, OperationResult operationResult)> GetByIdAsync(int id)
        {
            OperationResult operationResult;

            var platform = await _platformRepository
                .GetEntityWithoutTrackingAsync(platform => platform.Id == id);

            if (platform is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = PlatformResource.NotFoundSpecified,
                };

                return (new PlatformDto(), operationResult);
            }

            var platformDto = _mapper.Map<PlatformDto>(platform);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (platformDto, operationResult);
        }
    }
}
