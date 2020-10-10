using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Extensions;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Requests;
using SocialMediaDashboard.Web.Contracts.Responses;
using SocialMediaDashboard.Web.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(ApiRoutes.Subscription.Create, Name = nameof(CreateSubscription))]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            const string incorrectAccountName = "string";
            const int incorrectSubscriptionTypeId = 0;

            if (string.IsNullOrEmpty(request.AccountName)
                || request.AccountName == incorrectAccountName
                || request.SubscriptionTypeId == incorrectSubscriptionTypeId)
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
                    Error = operationResult.Message
                });
            }

            var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
            subscriptionSuccessfulResponse.Subscriptions.Add(subscriptionDto);
            subscriptionSuccessfulResponse.Message = operationResult.Message;

            // Must return Created
            return Ok(subscriptionSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Subscription.Get, Name = nameof(GetAccount))]
        public async Task<IActionResult> GetAccount(int id)
        {
            var userId = HttpContext.GetUserId();
            var(subscriptionDto, operationResult) = await _subscriptionService.GetSubscriptionByIdAsync(userId, id);

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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoutes.Subscription.Delete, Name = nameof(DeleteSubscription))]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var userId = HttpContext.GetUserId();

            var operationResult = await _subscriptionService.DeleteSubscriptionByIdAsync(userId, id);
            if (!operationResult.Result)
            {
                return NotFound(new SubscriptionFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            return NoContent();
        }





        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    [HttpGet(ApiRoutes.Account.Get, Name = nameof(GetAccountAsync))]
        //    public async Task<IActionResult> GetAccountAsync(int id)
        //    {
        //        var userId = HttpContext.GetUserId();
        //        var (accountDto, accountResult) = await _accountService.GetAccountByUserIdAsync(userId, id);
        //        if (!accountResult.Result)
        //        {
        //            return NotFound(new AccountFailedResponse
        //            {
        //                Error = accountResult.Message,
        //            });
        //        }

        //        var accountSuccessfulResponse = new AccountSuccessfulResponse();
        //        accountSuccessfulResponse.Accounts.Add(accountDto);
        //        accountSuccessfulResponse.Message = AccountResource.Successful;

        //        return Ok(accountSuccessfulResponse);
        //    }



        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    [HttpDelete(ApiRoutes.Account.Delete, Name = nameof(DeleteAccountAsync))]
        //    public async Task<IActionResult> DeleteAccountAsync(int id)
        //    {
        //        var userId = HttpContext.GetUserId();
        //        var operationResult = await _accountService.DeleteAccountByUserIdAsync(userId, id);
        //        if (!operationResult.Result)
        //        {
        //            return BadRequest(new AccountFailedResponse
        //            {
        //                Error = operationResult.Message,
        //            });
        //        }

        //        return NoContent();
        //    }





        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[HttpPost(ApiRoutes.Subscription.Create, Name = nameof(CreateSubscriptionAsync))]
        //public async Task<IActionResult> CreateSubscriptionAsync([FromBody] SubscriptionCreateRequest request)
        //{
        //    request = request ?? throw new ArgumentNullException(nameof(request));

        //    if (request.AccountId == 0 || request.SubscriptionType.ValidateSubscriptionType())
        //    {
        //        return BadRequest(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.IncorrectData
        //        });
        //    }

        //    var userId = HttpContext.GetUserId();
        //    var validateResult = await _accountService.ValidateAccountExistAsync(request.AccountId, userId);
        //    if (!validateResult.Result)
        //    {
        //        return NotFound(new SubscriptionFailedResponse
        //        {
        //            Error = validateResult.Message
        //        });
        //    }

        //    var (accountDto, accountOperationResult) = await _accountService.GetAccountByUserIdAsync(userId, request.AccountId);
        //    if (!accountOperationResult.Result)
        //    {
        //        return BadRequest(new SubscriptionFailedResponse
        //        {
        //            Error = accountOperationResult.Message
        //        });
        //    }

        //    var result = await _subscriptionService.AddSubscriptionAsync(userId, accountDto.Id, accountDto.Name, accountDto.Type, request.SubscriptionType);
        //    if (!result)
        //    {
        //        return Conflict(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.SubscriptionAddException
        //        });
        //    }

        //    return Ok(new SubscriptionSuccessfulResponse
        //    {
        //        Message = SubscriptionResource.SubscriptionAdded
        //    });
        //}

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpGet(ApiRoutes.Subscription.GetAll, Name = nameof(GetAllSubscriptionsAsync))]
        //public async Task<IActionResult> GetAllSubscriptionsAsync()
        //{
        //    var userId = HttpContext.GetUserId();
        //    var subscriptions = await _subscriptionService.GetAllUserSubscriptionsAsync(userId);

        //    if (!subscriptions.Any())
        //    {
        //        return NotFound(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.NotFound
        //        });
        //    }

        //    var subscriptionSuccessfulResponse = new SubscriptionSuccessfulResponse();
        //    subscriptionSuccessfulResponse.Subscriptions.AddRange(subscriptions);
        //    subscriptionSuccessfulResponse.Message = SubscriptionResource.Successful;

        //    return Ok(subscriptionSuccessfulResponse);
        //}

        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[HttpDelete(ApiRoutes.Subscription.Delete, Name = nameof(DeleteSubscriptionAsync))]
        //public async Task<IActionResult> DeleteSubscriptionAsync([FromBody] SubscriptionDeleteRequest request)
        //{
        //    request = request ?? throw new ArgumentNullException(nameof(request));

        //    if (request.Id == 0)
        //    {
        //        return BadRequest(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.IncorrectData
        //        });
        //    }

        //    var subscriptionExist = await _subscriptionService.SubscriptionExistAsync(request.Id);
        //    if (!subscriptionExist)
        //    {
        //        return NotFound(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.SubscriptionNotFound
        //        });
        //    }

        //    var userId = HttpContext.GetUserId();
        //    var result = await _subscriptionService.DeleteSubscriptionAsync(request.Id, userId);
        //    if (!result)
        //    {
        //        return Conflict(new SubscriptionFailedResponse
        //        {
        //            Error = SubscriptionResource.SubscriptionDeleteException
        //        });
        //    }

        //    return NoContent();
        //}
    }
}
