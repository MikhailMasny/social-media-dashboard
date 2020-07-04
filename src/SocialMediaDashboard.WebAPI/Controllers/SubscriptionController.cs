using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Resources;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using SocialMediaDashboard.WebAPI.Contracts.Responses;
using SocialMediaDashboard.WebAPI.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMediaService _mediaService;

        public SubscriptionController(ISubscriptionService subscriptionService,
                                      IMediaService mediaService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Subscription.Create, Name = nameof(CreateSubscriptionAsync))]
        public async Task<IActionResult> CreateSubscriptionAsync([FromBody] SubscriptionCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (request.MediaId == 0 || request.SubscriptionType == 0) // TODO: fix to check value
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = Subscription.IncorrectData
                });
            }

            var mediaExist = await _mediaService.AccountExistAsync(request.MediaId);
            if (!mediaExist)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = Subscription.MediaNotFound
                });
            }

            var userId = HttpContext.GetUserId();
            var media = await _mediaService.GetAccountAsync(request.MediaId);
            var result = await _subscriptionService.AddSubscriptionAsync(userId, media.Id, media.AccountName, media.Type, request.SubscriptionType);

            if (!result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = Subscription.SubscriptionAddException
                });
            }

            return Ok(new SubscriptionSuccessfulResponse
            {
                Message = Subscription.SubscriptionAdded
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Subscription.GetAll, Name = nameof(GetAllSubscriptionsAsync))]
        public async Task<IActionResult> GetAllSubscriptionsAsync()
        {
            var userId = HttpContext.GetUserId();
            var subscriptions = await _subscriptionService.GetAllUserSubscriptionsAsync(userId);

            if (!subscriptions.Any())
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = Subscription.NotFound
                });
            }

            var mediaSuccessfulResponse = new SubscriptionSuccessfulResponse();
            mediaSuccessfulResponse.Subscriptions.AddRange(subscriptions);
            mediaSuccessfulResponse.Message = Subscription.Successful;

            return Ok(mediaSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpDelete(ApiRoutes.Subscription.Delete, Name = nameof(DeleteSubscriptionAsync))]
        public async Task<IActionResult> DeleteSubscriptionAsync([FromBody] SubscriptionDeleteRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (request.Id == 0)
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = Subscription.IncorrectData
                });
            }

            var subscriptionExist = await _subscriptionService.SubscriptionExistAsync(request.Id);
            if (!subscriptionExist)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = Subscription.NotFound
                });
            }

            var userId = HttpContext.GetUserId();
            var result = await _subscriptionService.DeleteSubscriptionAsync(request.Id, userId);
            if (!result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = Subscription.SubscriptionDeleteException
                });
            }

            return Ok(new SubscriptionSuccessfulResponse
            {
                Message = Subscription.SubscriptionDeleted
            });
        }
    }
}
