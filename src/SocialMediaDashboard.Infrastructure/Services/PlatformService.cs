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
    /// <inheritdoc cref="IPlatformService"/>
    public class PlatformService : IPlatformService
    {
        private readonly IGenericRepository<Platform> _platformRepository;
        private readonly IMapper _mapper;

        public PlatformService(IGenericRepository<Platform> platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository ?? throw new ArgumentNullException(nameof(platformRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlatformDto>> GetAllAsync()
        {
            var platforms = await _platformRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            if (!platforms.Any())
            {
                throw new NotFoundException(PlatformResource.NotFound);
            }

            return _mapper.Map<List<PlatformDto>>(platforms);
        }

        public async Task<PlatformDto> GetByIdAsync(int id)
        {
            var platform = await _platformRepository
                .GetEntityWithoutTrackingAsync(platform => platform.Id == id);

            if (platform is null)
            {
                throw new NotFoundException(PlatformResource.NotFoundSpecified);
            }

            return _mapper.Map<PlatformDto>(platform);
        }
    }
}
