using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IObservationService"/>
    public class ObservationService : IObservationService
    {
        private readonly IRepository<Observation> _observationRepository;
        private readonly IMapper _mapper;

        public ObservationService(IRepository<Observation> observationRepository,
                                  IMapper mapper)
        {
            _observationRepository = observationRepository ?? throw new ArgumentNullException(nameof(observationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ObservationDto>> GetAllAsync()
        {
            var observations = await _observationRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            return _mapper.Map<List<ObservationDto>>(observations);
        }

        public async Task<ObservationDto> GetByIdAsync(int id)
        {
            var observation = await _observationRepository
                .GetEntityWithoutTrackingAsync(observation => observation.Id == id);

            if (observation is null)
            {
                // TODO: message
                return null;
            }

            return _mapper.Map<ObservationDto>(observation);
        }
    }
}
