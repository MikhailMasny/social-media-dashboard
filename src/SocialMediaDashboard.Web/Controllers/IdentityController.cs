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
using SocialMediaDashboard.Web.Extensions;
using SocialMediaDashboard.Web.Models;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ISenderService _senderService;

        public IdentityController(
            IIdentityService identityService,
            ISenderService senderService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _senderService = senderService ?? throw new ArgumentNullException(nameof(senderService));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoute.Identity.SignUp, Name = nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var code =
                await _identityService.SignUpAsync(
                    request.Email,
                    request.Password,
                    request.Name);

            await _senderService.RenderAndSendAsync(
                new EmailViewModel
                {
                    Name = request.Name,
                    Link = Url.Action(
                    "ConfirmEmail",
                    "Identity",
                    new
                    {
                        request.Email,
                        Code = code.EncodeToken(),
                    },
                    protocol: HttpContext.Request.Scheme),
                },
                "Views/Mail/Confirm.cshtml",
                request.Email,
                EmailResource.AccountCreated);

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

            var authenticationResult =
                await _identityService.SignInAsync(
                    request.Email,
                    request.Password);

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

            var authenticationResult =
                await _identityService.ConfirmEmailAsync(
                    query.Email,
                    query.Code.DecodeToken());

            //TODO: Change it to real domain
            //var queryString = new
            //{
            //    confirmEmail = true
            //};
            //return Redirect($"{domain}{queryString}"); 

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            var passwordResetToken = confirmationResult.Code.EncodeToken();

            var callbackUrl = Url.Action(
                "ResetPassword",
                "Identity",
                new
                {
                    request.Email,
                    Code = passwordResetToken
                },
                protocol: HttpContext.Request.Scheme);

            var emailViewModel = new EmailViewModel
            {
                Name = confirmationResult.Data,
                Link = callbackUrl
            };

            await _senderService.RenderAndSendAsync(
                emailViewModel,
                "Views/Mail/Restore.cshtml",
                request.Email,
                EmailResource.PasswordReset);

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
            //TODO: Change it to real domain
            //var queryString = HttpContext.Request.QueryString.Value;
            //return Redirect($"{domain}{queryString}"); 
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

            var authenticationResult =
                await _identityService.ResetPasswordAsync(
                    request.Email,
                    passwordRequest.Password,
                    request.Code.DecodeToken());

            await _senderService.RenderAndSendAsync(
                new EmailViewModel
                {
                    Name = request.Email,
                },
                "Views/Mail/Reset.cshtml",
                request.Email,
                EmailResource.PasswordChanged);

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                Message = IdentityResource.PasswordAccepted
            });
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoute.Identity.Refresh, Name = nameof(RefreshToken))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var authenticationResult =
                await _identityService.RefreshTokenAsync(
                    request.Token,
                    request.RefreshToken);

            return Ok(new AuthSuccessfulResponse
            {
                Token = authenticationResult.Token,
                RefreshToken = authenticationResult.RefreshToken,
                Message = IdentityResource.EmailConfirm
            });
        }
    }
}
