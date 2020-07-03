using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Resources;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using SocialMediaDashboard.WebAPI.Contracts.Responses;
using SocialMediaDashboard.WebAPI.Extensions;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost(ApiRoutes.Media.Create, Name = nameof(CreateAsync))]
        public async Task<IActionResult> CreateAsync([FromBody] MediaCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.AccountName))
            {
                return BadRequest(new MediaFailedResponse
                {
                    Error = Media.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var mediaDto = await _mediaService.AddUserAccount(userId, request.AccountName);

            if (mediaDto == null)
            {
                return Conflict(new MediaFailedResponse
                {
                    Error = Media.AccountAlreadyExist
                });
            }

            return Ok(new MediaSuccessfulResponse
            {
                AccountName = mediaDto.AccountName,
                Message = Media.AccountAdded
            });
        }
    }
}
