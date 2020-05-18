using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Interfaces;
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
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResult = await _identityService.CreateUserAsync(request.Email, request.Password);

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
