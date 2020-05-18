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
            var authResult = await _identityService.RegistrationAsync(request.Email, request.Password);

            if (!authResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessfulResponse
            {
                Token = authResult.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResult = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessfulResponse
            {
                Token = authResult.Token
            });
        }

        [HttpGet(ApiRoutes.Identity.Confirm)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailQuery emailQuery)
        {
            if (emailQuery.UserId == null || emailQuery.Code == null)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Data is incorrect." }
                });
            }

            var authResult = await _identityService.ConfirmEmailAsync(emailQuery.UserId, emailQuery.Code);

            if (!authResult.IsSuccessful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResult.Errors
                });
            }

            return Ok(new AuthSuccessfulResponse
            {
                Token = authResult.Token
            });
        }
    }
}
