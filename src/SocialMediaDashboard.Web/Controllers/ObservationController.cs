using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private readonly IObservationService _observationService;

        public ObservationController(IObservationService observationService)
        {
            _observationService = observationService ?? throw new ArgumentNullException(nameof(observationService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Observation.Get, Name = nameof(GetAllObservation))]
        public async Task<IActionResult> GetAllObservation(int id)
        {
            var (observationDto, operationResult) = await _observationService.GetByIdAsync(id);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var observationSuccessfulResponse = new SuccessfulResponse<ObservationDto>();
            observationSuccessfulResponse.Items.Add(observationDto);
            observationSuccessfulResponse.Message = CommonResource.Successful;

            return Ok(observationSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Observation.GetAll, Name = nameof(GetAllObservations))]
        public async Task<IActionResult> GetAllObservations()
        {
            var (observationDtos, operationResult) = await _observationService.GetAllAsync();
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var observationSuccessfulResponse = new SuccessfulResponse<ObservationDto>();
            observationSuccessfulResponse.Items.AddRange(observationDtos);
            observationSuccessfulResponse.Message = CommonResource.Successful;

            return Ok(observationSuccessfulResponse);
        }
    }
}
