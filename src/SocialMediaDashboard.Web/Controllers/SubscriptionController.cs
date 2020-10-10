﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
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
        [HttpPost(ApiRoutes.Subscription.Create, Name = nameof(CreateSubscription))]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            const string incorrectAccountName = "string";
            const int incorrectSubscriptionTypeId = 0;

            var subscriptionTypeExist = await _subscriptionTypeService.SubscriptionTypeExistAsync(request.SubscriptionTypeId);

            if (string.IsNullOrEmpty(request.AccountName)
                || request.AccountName == incorrectAccountName
                || request.SubscriptionTypeId == incorrectSubscriptionTypeId
                || !subscriptionTypeExist)
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.CreateSubscriptionAsync(userId, request.AccountName, request.SubscriptionTypeId);
            if (!operationResult.Result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var uri = new Uri($"{Request.Scheme}://{Request.Host}/{ApiRoutes.Subscription.Create}/{subscriptionDto.Id}");
            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Created(uri, subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Subscription.Get, Name = nameof(GetSubscription))]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.GetSubscriptionByIdAsync(id, userId);
            if (!operationResult.Result)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Subscription.GetAll, Name = nameof(GetAllSubscriptions))]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var userId = HttpContext.GetUserId();
            var (subscriptionDtos, operationResult) = await _subscriptionService.GetAllSubscriptionAsync(userId);
            if (!operationResult.Result)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.AddRange(subscriptionDtos);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut(ApiRoutes.Subscription.Update, Name = nameof(UpdateSubscription))]
        public async Task<IActionResult> UpdateSubscription(int id, [FromBody] SubscriptionCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            const string incorrectAccountName = "string";
            const int incorrectSubscriptionTypeId = 0;

            var subscriptionTypeExist = await _subscriptionTypeService.SubscriptionTypeExistAsync(request.SubscriptionTypeId);

            if (string.IsNullOrEmpty(request.AccountName)
                || request.AccountName == incorrectAccountName
                || request.SubscriptionTypeId == incorrectSubscriptionTypeId
                || !subscriptionTypeExist)
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var (subscriptionDto, operationResult) = await _subscriptionService.UpdateSubscriptionAsync(id, userId, request.AccountName, request.SubscriptionTypeId);
            if (!operationResult.Result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoutes.Subscription.Delete, Name = nameof(DeleteSubscription))]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var userId = HttpContext.GetUserId();

            var operationResult = await _subscriptionService.DeleteSubscriptionByIdAsync(id, userId);
            if (!operationResult.Result)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            return NoContent();
        }
    }
}
