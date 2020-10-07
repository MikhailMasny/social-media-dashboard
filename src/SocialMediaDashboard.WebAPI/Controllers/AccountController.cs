using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Account.Create, Name = nameof(CreateAccountAsync))]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Name) || request.AccountType.ValidateAccountType())
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = AccountResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var (accountDto, operationResult) = await _accountService.CreateAccountByUserIdAsync(userId, request.Name, request.AccountType);
            if (!operationResult.Result)
            {
                return Conflict(new AccountFailedResponse
                {
                    Error = operationResult.Message
                });
            }

            var accountSuccessfulResponse = new AccountSuccessfulResponse();
            accountSuccessfulResponse.Accounts.Add(accountDto);
            accountSuccessfulResponse.Message = operationResult.Message;

            // Must return Created
            return Ok(accountSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Account.Get, Name = nameof(GetAccountAsync))]
        public async Task<IActionResult> GetAccountAsync(int id)
        {
            var userId = HttpContext.GetUserId();
            var (accountDto, accountResult) = await _accountService.GetAccountByUserIdAsync(userId, id);
            if (!accountResult.Result)
            {
                return NotFound(new AccountFailedResponse
                {
                    Error = accountResult.Message,
                });
            }

            var accountSuccessfulResponse = new AccountSuccessfulResponse();
            accountSuccessfulResponse.Accounts.Add(accountDto);
            accountSuccessfulResponse.Message = AccountResource.Successful;

            return Ok(accountSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Account.GetAll, Name = nameof(GetAllAccountsAsync))]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var userId = HttpContext.GetUserId();
            var (accountDtos, operationResult) = await _accountService.GetAllUserAccountsAsync(userId);
            if (!operationResult.Result)
            {
                return NotFound(new AccountFailedResponse
                {
                    Error = operationResult.Message
                });
            }

            var accountSuccessfulResponse = new AccountSuccessfulResponse();
            accountSuccessfulResponse.Accounts.AddRange(accountDtos);
            accountSuccessfulResponse.Message = operationResult.Message;

            return Ok(accountSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut(ApiRoutes.Account.Update, Name = nameof(UpdateAccountAsync))]
        public async Task<IActionResult> UpdateAccountAsync(int id, [FromBody] AccountCreateOrUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Name) || request.AccountType.ValidateAccountType())
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = AccountResource.IncorrectData,
                });
            }

            var userId = HttpContext.GetUserId();
            var operationResult = await _accountService.UpdateAccountByUserIdAsync(userId, id, request.Name, request.AccountType);
            if (!operationResult.Result)
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoutes.Account.Delete, Name = nameof(DeleteAccountAsync))]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            var userId = HttpContext.GetUserId();
            var operationResult = await _accountService.DeleteAccountByUserIdAsync(userId, id);
            if (!operationResult.Result)
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            return NoContent();
        }
    }
}
