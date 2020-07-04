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
        [HttpPost(ApiRoutes.Media.Create, Name = nameof(CreateMediaAsync))]
        public async Task<IActionResult> CreateMediaAsync([FromBody] MediaCreateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.AccountName) || request.AccountType == 0) // TODO: fix to check value
            {
                return BadRequest(new MediaFailedResponse
                {
                    Error = Media.IncorrectData
                });
            }

            var userId = HttpContext.GetUserId();
            var result = await _mediaService.AddAccountAsync(userId, request.AccountName, request.AccountType);

            if (!result)
            {
                return Conflict(new MediaFailedResponse
                {
                    Error = Media.AccountAddException
                });
            }

            return Ok(new MediaSuccessfulResponse
            {
                Message = Media.AccountAdded
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoutes.Media.GetAll, Name = nameof(GetAllMediaAsync))]
        public async Task<IActionResult> GetAllMediaAsync()
        {
            var userId = HttpContext.GetUserId();
            var media = await _mediaService.GetAllUserAccountsAsync(userId);

            if (!media.Any())
            {
                return NotFound(new MediaFailedResponse
                {
                    Error = Media.NotFound
                });
            }

            var mediaSuccessfulResponse = new MediaSuccessfulResponse();
            mediaSuccessfulResponse.MediaAll.AddRange(media);
            mediaSuccessfulResponse.Message = Media.Successful;

            return Ok(mediaSuccessfulResponse);
        }
    }
}
