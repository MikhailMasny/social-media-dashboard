using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
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
        [HttpGet(ApiRoute.SubscriptionType.Get, Name = nameof(GetSubscriptionType))]
        public async Task<IActionResult> GetSubscriptionType(int id)
        {
            var (subscriptionTypeDto, operationResult) = await _subscriptionTypeService.GetByIdAsync(id);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionTypeSuccessfulResponse = new SuccessfulResponse<SubscriptionTypeDto>();
            subscriptionTypeSuccessfulResponse.Items.Add(subscriptionTypeDto);
            subscriptionTypeSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionTypeSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.SubscriptionType.GetAll, Name = nameof(GetAllSubscriptionTypes))]
        public async Task<IActionResult> GetAllSubscriptionTypes()
        {
            var (subscriptionTypeDtos, operationResult) = await _subscriptionTypeService.GetAllAsync();
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionTypeSuccessfulResponse = new SuccessfulResponse<SubscriptionTypeDto>();
            subscriptionTypeSuccessfulResponse.Items.AddRange(subscriptionTypeDtos);
            subscriptionTypeSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionTypeSuccessfulResponse);
        }
    }
}
