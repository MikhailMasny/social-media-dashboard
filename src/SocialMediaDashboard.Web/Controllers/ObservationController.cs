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
using System.Collections.Generic;
using System.Linq;
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
            return Ok(new SuccessfulResponse<ObservationDto>
            {
                Message = CommonResource.Successful,
                Items = new List<ObservationDto>
                {
                    await _observationService.GetByIdAsync(id),
                },
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Observation.GetAll, Name = nameof(GetAllObservations))]
        public async Task<IActionResult> GetAllObservations()
        {
            return Ok(new SuccessfulResponse<ObservationDto>
            {
                Message = CommonResource.Successful,
                Items = (await _observationService.GetAllAsync()).ToList(),
            });
        }
    }
}
