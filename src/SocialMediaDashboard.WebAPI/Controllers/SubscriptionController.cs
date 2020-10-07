using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Extensions;
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
        private readonly IAccountService _accountService;

        public SubscriptionController(ISubscriptionService subscriptionService,
                                      IAccountService accountService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Subscription.Create, Name = nameof(CreateSubscriptionAsync))]
        public async Task<IActionResult> CreateSubscriptionAsync([FromBody] SubscriptionCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (request.AccountId == 0 || request.SubscriptionType.ValidateSubscriptionType())
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var validateResult = await _accountService.ValidateAccountExistAsync(request.AccountId, userId);
            if (!validateResult.Result)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = validateResult.Message
                });
            }

            var (accountDto, accountOperationResult) = await _accountService.GetAccountByUserIdAsync(userId, request.AccountId);
            if (!accountOperationResult.Result)
            {
                return BadRequest(new SubscriptionFailedResponse
                {
                    Error = accountOperationResult.Message
                });
            }

            var result = await _subscriptionService.AddSubscriptionAsync(userId, accountDto.Id, accountDto.Name, accountDto.Type, request.SubscriptionType);
            if (!result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.SubscriptionAddException
                });
            }

            return Ok(new SubscriptionSuccessfulResponse
            {
                Message = SubscriptionResource.SubscriptionAdded
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
                    Error = SubscriptionResource.NotFound
                });
            }

            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.AddRange(subscriptions);
            subscriptionSuccessfulResponse.Message = SubscriptionResource.Successful;

            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                    Error = SubscriptionResource.IncorrectData
                });
            }

            var subscriptionExist = await _subscriptionService.SubscriptionExistAsync(request.Id);
            if (!subscriptionExist)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.SubscriptionNotFound
                });
            }

            var userId = HttpContext.GetUserId();
            var result = await _subscriptionService.DeleteSubscriptionAsync(request.Id, userId);
            if (!result)
            {
                return Conflict(new SubscriptionFailedResponse
                {
                    Error = SubscriptionResource.SubscriptionDeleteException
                });
            }

            return NoContent();
        }
    }
}
