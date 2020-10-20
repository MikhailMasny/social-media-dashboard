using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Queries;
using SocialMediaDashboard.Web.Contracts.Requests;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public IdentityController(IIdentityService identityService,
                                  IEmailService emailService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoute.Identity.SignUp, Name = nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var confirmationResult = await _identityService.SignUpAsync(request.Email, request.Password, request.Name);
            if (!confirmationResult.IsSuccessful)
            {
                return Conflict(new AuthFailedResponse
                {
                    Errors = confirmationResult.Errors
                });
            }

            // TODO: bug?!
            var encodedToken = Uri.EscapeDataString(confirmationResult.Code);

            var callbackUrl = Url.Action(
                "ConfirmEmail", "Identity",
                new { request.Email, token = encodedToken },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendMessageAsync(request.Email, IdentityResource.EmailConfirm, callbackUrl); // TODO: Razor Service

            return Ok(new AuthSuccessfulResponse
            {
                Message = IdentityResource.EmailConfirm
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoute.Identity.SignIn, Name = nameof(SignIn))]
        public async Task<IActionResult> SignIn([FromBody] UserSignInRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var authenticationResult = await _identityService.SignInAsync(request.Email, request.Password);

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
        [HttpGet(ApiRoute.Identity.Confirm, Name = nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailQuery query)
        {
            query = query ?? throw new ArgumentNullException(nameof(query));

            if (query.Email is null || query.Code is null)
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
        [HttpPost(ApiRoute.Identity.Restore, Name = nameof(RestorePassword))]
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

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(confirmationResult.Code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            // TODO: bug?!
            var callbackUrl = Url.Action(
                "ResetPassword", "Identity",
                new { email = request.Email, token = codeEncoded },
                protocol: HttpContext.Request.Scheme);

            var text = $"{callbackUrl}\n\n{confirmationResult.Code}";

            await _emailService.SendMessageAsync(request.Email, IdentityResource.PasswordResetting, text); // TODO: Razor Service

            return Ok(new AuthSuccessfulResponse
            {
                Message = IdentityResource.PasswordResetting
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet(ApiRoute.Identity.Reset, Name = nameof(ResetPassword))]
        public IActionResult ResetPassword()
        {
            var queryString = HttpContext.Request.QueryString.Value;
            //return Redirect($"{domain}{queryString}"); //TODO: Change it to real domain
            return Ok();
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(ApiRoute.Identity.Reset, Name = nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromQuery] UserResetPasswordRequest request, [FromBody] UserNewPasswordRequest passwordRequest)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            passwordRequest = passwordRequest ?? throw new ArgumentNullException(nameof(passwordRequest));

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(request.Code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var authenticationResult = await _identityService.ResetPasswordAsync(request.Email, passwordRequest.Password, codeDecoded);

            if (!authenticationResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authenticationResult.Errors
                });
            }

            await _emailService.SendMessageAsync(request.Email, "Password was changed", "Password was changed"); // TODO: literal

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = IdentityResource.PasswordAccepted
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoute.Identity.Refresh, Name = nameof(RefreshToken))]
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

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken,
                Message = IdentityResource.EmailConfirm
            });
        }
    }
}
