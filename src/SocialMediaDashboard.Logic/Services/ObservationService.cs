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

        public async Task<(IEnumerable<ObservationDto> observationDtos, OperationResult operationResult)> GetAllAsync()
        {
            OperationResult operationResult;

            var observations = await _observationRepository
                .GetAllWithoutTracking()
                .ToListAsync();

            if (!observations.Any())
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = ObservationResource.NotFound,
                };

                return (new List<ObservationDto>(), operationResult);
            }

            var observationsDtos = _mapper.Map<List<ObservationDto>>(observations);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (observationsDtos, operationResult);
        }

        public async Task<(ObservationDto observationDto, OperationResult operationResult)> GetByIdAsync(int id)
        {
            OperationResult operationResult;

            var observation = await _observationRepository
                .GetEntityWithoutTrackingAsync(observation => observation.Id == id);

            if (observation is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = ObservationResource.NotFoundSpecified,
                };

                return (new ObservationDto(), operationResult);
            }

            var observationDto = _mapper.Map<ObservationDto>(observation);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (observationDto, operationResult);
        }
    }
}
