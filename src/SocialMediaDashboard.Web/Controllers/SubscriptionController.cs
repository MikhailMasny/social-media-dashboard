using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Requests;
using SocialMediaDashboard.Web.Contracts.Responses;
using SocialMediaDashboard.Web.Extensions;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public SubscriptionController(ISubscriptionService subscriptionService,
                                      ISubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _subscriptionTypeService = subscriptionTypeService ?? throw new ArgumentNullException(nameof(subscriptionTypeService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoute.Subscription.Create, Name = nameof(CreateSubscription))]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var subscriptionTypeExist = await _subscriptionTypeService.IsExistAsync(request.SubscriptionTypeId);
            if (!subscriptionTypeExist)
            {
                return BadRequest(new FailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.CreateAsync(userId, request.AccountName, request.SubscriptionTypeId);
            if (!operationResult.Result)
            {
                return Conflict(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var uri = new Uri($"{Request.Scheme}://{Request.Host}/{ApiRoute.Subscription.Create}/{subscriptionDto.Id}");
            var subscriptionSuccessfulResponse = new SuccessfulResponse<SubscriptionDto>();
            subscriptionSuccessfulResponse.Items.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Created(uri, subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Subscription.Get, Name = nameof(GetSubscription))]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.GetByIdAsync(id, userId);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SuccessfulResponse<SubscriptionDto>();
            subscriptionSuccessfulResponse.Items.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Subscription.GetAll, Name = nameof(GetAllSubscriptions))]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var userId = HttpContext.GetUserId();
            var (subscriptionDtos, operationResult) = await _subscriptionService.GetAllAsync(userId);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SuccessfulResponse<SubscriptionDto>();
            subscriptionSuccessfulResponse.Items.AddRange(subscriptionDtos);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut(ApiRoute.Subscription.Update, Name = nameof(UpdateSubscription))]
        public async Task<IActionResult> UpdateSubscription(int id, [FromBody] SubscriptionCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var subscriptionTypeExist = await _subscriptionTypeService.IsExistAsync(request.SubscriptionTypeId);
            if (!subscriptionTypeExist)
            {
                return BadRequest(new FailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.UpdateAsync(id, userId, request.AccountName, request.SubscriptionTypeId);
            if (!operationResult.Result)
            {
                return Conflict(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SuccessfulResponse<SubscriptionDto>();
            subscriptionSuccessfulResponse.Items.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoute.Subscription.Delete, Name = nameof(DeleteSubscription))]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var userId = HttpContext.GetUserId();

            var operationResult = await _subscriptionService.DeleteByIdAsync(id, userId);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            return NoContent();
        }
    }
}
