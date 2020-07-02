using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Resources;
using SocialMediaDashboard.WebAPI.Contracts.Queries;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using SocialMediaDashboard.WebAPI.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Identity.Registration, Name = nameof(Registration))]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationRequest request)
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
                Message = $"{Identity.EmailConfirm} Code: {confirmationResult.Code}"
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Login, Name = nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
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
                Message = Identity.EmailAndPasswordAccepted
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(ApiRoutes.Identity.Confirm, Name = nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailQuery query)
        {
            query = query ?? throw new ArgumentNullException(nameof(query));

            if (query.Email == null || query.Code == null)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { Identity.IncorrectData }
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
                Message = Identity.EmailConfirmed
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Restore, Name = nameof(RestorePassword))]
        public async Task<IActionResult> RestorePassword([FromBody] UserRestorePasswordRequest request)
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
                Message = $"{Identity.PasswordResetting} Code: {confirmationResult.Code}" // UNDONE: RazorViewEngine + SendGrid
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoutes.Identity.Reset, Name = nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequest request)
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
                Message = Identity.PasswordAccepted // UNDONE: RazorViewEngine + SendGrid
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Identity.Refresh, Name = nameof(RefreshToken))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
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
                Message = Identity.EmailConfirm
            });
        }
    }
}
