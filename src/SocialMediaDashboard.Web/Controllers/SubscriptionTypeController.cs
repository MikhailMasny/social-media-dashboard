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
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public SubscriptionTypeController(ISubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionTypeService = subscriptionTypeService ?? throw new ArgumentNullException(nameof(subscriptionTypeService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.SubscriptionTypeRoute.Get, Name = nameof(GetSubscriptionType))]
        public async Task<IActionResult> GetSubscriptionType(int id)
        {
            return Ok(new SuccessfulResponse<SubscriptionTypeDto>
            {
                Message = CommonResource.Successful,
                Items = new List<SubscriptionTypeDto>
                {
                    await _subscriptionTypeService.GetByIdAsync(id),
                },
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.SubscriptionTypeRoute.GetAll, Name = nameof(GetAllSubscriptionTypes))]
        public async Task<IActionResult> GetAllSubscriptionTypes()
        {
            return Ok(new SuccessfulResponse<SubscriptionTypeDto>
            {
                Message = CommonResource.Successful,
                Items = (await _subscriptionTypeService.GetAllAsync()).ToList(),
            });
        }
    }
}
