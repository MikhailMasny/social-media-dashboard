using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Queries;
using SocialMediaDashboard.Web.Contracts.Requests;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Identity.Registration, Name = nameof(RegistrationAsync))]
        public async Task<IActionResult> RegistrationAsync([FromBody] UserRegistrationRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var confirmationResult = await _identityService.RegistrationAsync(request.Email, request.UserName, request.Password);

            if (!confirmationResult.IsSuccessful)
            {
                return Conflict(new AuthFailedResponse
                {
                    Errors = confirmationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new AuthSuccessfulResponse
            {
                Message = $"{IdentityResource.EmailConfirm} Code: {confirmationResult.Code}"
            });
        }

        // TODO: change it to SignIn
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Login, Name = nameof(LoginAsync))]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var authenticationResult = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken,
                Message = IdentityResource.EmailAndPasswordAccepted
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(ApiRoutes.Identity.Confirm, Name = nameof(ConfirmEmailAsync))]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] EmailQuery query)
        {
            query = query ?? throw new ArgumentNullException(nameof(query));

            if (query.Email == null || query.Code == null)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { IdentityResource.IncorrectData }
                });
            }

            var authenticationResult = await _identityService.ConfirmEmailAsync(query.Email, query.Code);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken,
                Message = IdentityResource.EmailConfirmed
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Restore, Name = nameof(RestorePasswordAsync))]
        public async Task<IActionResult> RestorePasswordAsync([FromBody] UserRestorePasswordRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var confirmationResult = await _identityService.RestorePasswordAsync(request.Email);

            if (!confirmationResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = confirmationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new AuthSuccessfulResponse
            {
                Message = $"{IdentityResource.PasswordResetting} Code: {confirmationResult.Code}" // UNDONE: RazorViewEngine + SendGrid
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Reset, Name = nameof(ResetPasswordAsync))]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] UserResetPasswordRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var authenticationResult = await _identityService.ResetPasswordAsync(request.Email, request.NewPassword, request.Code);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = IdentityResource.PasswordAccepted // UNDONE: RazorViewEngine + SendGrid
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Identity.Refresh, Name = nameof(RefreshTokenAsync))]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var authenticationResult = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authenticationResult.IsSuccessful)
            {
                return Conflict(new AuthFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken,
                Message = IdentityResource.EmailConfirm
            });
        }
    }
}
