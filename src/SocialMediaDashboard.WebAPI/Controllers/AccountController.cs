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
        // CRUD
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Account.Create, Name = nameof(CreateAccountAsync))]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Name) || request.AccountType.CheckAccountValue())
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = AccountResource.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var result = await _accountService.AddAccountAsync(userId, request.Name, request.AccountType);

            if (!result)
            {
                return Conflict(new AccountFailedResponse
                {
                    Error = AccountResource.Exception
                });
            }

            // Must return Created
            return Ok(new AccountSuccessfulResponse
            {
                Message = AccountResource.Added
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Account.GetAll, Name = nameof(GetAllAccountsByUserIdAsync))]
        public async Task<IActionResult> GetAllAccountsByUserIdAsync()
        {
            var userId = HttpContext.GetUserId();
            var accounts = await _accountService.GetAllUserAccountsAsync(userId);

            if (!accounts.Any())
            {
                return NotFound(new AccountFailedResponse
                {
                    Error = AccountResource.NotFound
                });
            }

            var accountSuccessfulResponse = new AccountSuccessfulResponse();
            accountSuccessfulResponse.Accounts.AddRange(accounts);
            accountSuccessfulResponse.Message = AccountResource.Successful;

            return Ok(accountSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(ApiRoutes.Account.Delete + "/{id}", Name = nameof(DeleteAccountAsync))]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            var userId = HttpContext.GetUserId();
            var userRole = HttpContext.GetUserRole();

            var operationResult = await _accountService.DeleteAccountAsync(userId, userRole, id);
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
