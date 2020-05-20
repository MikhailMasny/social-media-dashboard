using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Interfaces;
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

        [HttpPost(ApiRoutes.Identity.Registration)]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationRequest request)
        {
            var confirmationResult = await _identityService.RegistrationAsync(request.Email, request.Password);

            if (!confirmationResult.IsSuccessful)
            {
                return Conflict(new FailedResponse
                {
                    Errors = confirmationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new SuccessfulResponse
            {
                Message = $"For successful login confirm your email. Code: {confirmationResult.Code}"
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authenticationResult = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            return Ok(new SuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = "Email and password successfully accepted."
            });
        }

        [HttpGet(ApiRoutes.Identity.Confirm)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailQuery query)
        {
            if (query.Email == null || query.Code == null)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = new[] { "Data is incorrect." }
                });
            }

            var authenticationResult = await _identityService.ConfirmEmailAsync(query.Email, query.Code);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            return Ok(new SuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = "Your mail has been successfully confirmed."
            });
        }

        [HttpPost(ApiRoutes.Identity.Restore)]
        public async Task<IActionResult> RestorePassword([FromBody] UserRestorePasswordRequest request)
        {
            var confirmationResult = await _identityService.RestorePasswordAsync(request.Email);

            if (!confirmationResult.IsSuccessful)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = confirmationResult.Errors
                });
            }

            // UNDONE: send mail with token here
            
            return Ok(new SuccessfulResponse
            {
                Message = $"To continue resetting the password, follow the link sent to the mail.. Code: {confirmationResult.Code}" // UNDONE: RazorViewEngine + SendGrid
            });
        }

        [HttpPost(ApiRoutes.Identity.Reset)]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequest request)
        {
            var authenticationResult = await _identityService.ResetPasswordAsync(request.Email, request.NewPassword, request.Code);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            // UNDONE: send mail with token here

            return Ok(new SuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = "New password successfully accepted." // UNDONE: RazorViewEngine + SendGrid
            });
        }
    }
}
