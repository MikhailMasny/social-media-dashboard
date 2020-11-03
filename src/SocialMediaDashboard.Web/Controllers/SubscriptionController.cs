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
using System.Collections.Generic;
using System.Linq;
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

            var subscriptionTypeExist =
                await _subscriptionTypeService.IsExistAsync(request.SubscriptionTypeId);

            if (!subscriptionTypeExist)
            {
                return BadRequest(new FailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var subscriptionDto =
                await _subscriptionService.CreateAsync(
                    HttpContext.GetUserId(),
                    request.AccountName,
                    request.SubscriptionTypeId);

            return Created(
                new Uri($"{Request.Scheme}://{Request.Host}/{ApiRoute.Subscription.Create}/{subscriptionDto.Id}"),
                new SuccessfulResponse<SubscriptionDto>
                {
                    Message = SubscriptionResource.Added,
                    Items = new List<SubscriptionDto>
                    {
                        subscriptionDto,
                    },
                });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Subscription.Get, Name = nameof(GetSubscription))]
        public async Task<IActionResult> GetSubscription(int id)
        {
            return Ok(new SuccessfulResponse<SubscriptionDto>
            {
                Message = CommonResource.Successful,
                Items = new List<SubscriptionDto>
                {
                    await _subscriptionService.GetByIdAsync(
                        id,
                        HttpContext.GetUserId()),
                },
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Subscription.GetAll, Name = nameof(GetAllSubscriptions))]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            return Ok(new SuccessfulResponse<SubscriptionDto>
            {
                Message = CommonResource.Successful,
                Items = (await _subscriptionService.GetAllAsync(HttpContext.GetUserId())).ToList(),
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut(ApiRoute.Subscription.Update, Name = nameof(UpdateSubscription))]
        public async Task<IActionResult> UpdateSubscription(int id, [FromBody] SubscriptionCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var subscriptionTypeExist =
                await _subscriptionTypeService.IsExistAsync(request.SubscriptionTypeId);

            if (!subscriptionTypeExist)
            {
                return BadRequest(new FailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            return Ok(new SuccessfulResponse<SubscriptionDto>
            {
                Message = SubscriptionResource.Updated,
                Items = new List<SubscriptionDto>
                {
                    await _subscriptionService.UpdateAsync(
                        id,
                        HttpContext.GetUserId(),
                        request.AccountName,
                        request.SubscriptionTypeId)
                },
            });
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoute.Subscription.Delete, Name = nameof(DeleteSubscription))]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            await _subscriptionService.DeleteByIdAsync(
                id,
                HttpContext.GetUserId());

            return NoContent();
        }
    }
}
