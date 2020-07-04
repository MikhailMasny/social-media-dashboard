using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class AccountController : ControllerBase
    {
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

            if (string.IsNullOrEmpty(request.Name) || request.AccountType == 0) // TODO: fix to check value
            {
                return BadRequest(new AccountFailedResponse
                {
                    Error = Account.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var result = await _accountService.AddAccountAsync(userId, request.Name, request.AccountType);

            if (!result)
            {
                return Conflict(new AccountFailedResponse
                {
                    Error = Account.AccountAddException
                });
            }

            return Ok(new AccountSuccessfulResponse
            {
                Message = Account.AccountAdded
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
                    Error = Account.NotFound
                });
            }

            var accountSuccessfulResponse = new AccountSuccessfulResponse();
            accountSuccessfulResponse.Accounts.AddRange(accounts);
            accountSuccessfulResponse.Message = Account.Successful;

            return Ok(accountSuccessfulResponse);
        }
    }
}
