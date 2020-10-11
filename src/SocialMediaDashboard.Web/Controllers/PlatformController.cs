using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Resources;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService ?? throw new ArgumentNullException(nameof(platformService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Platform.Get, Name = nameof(GetPlatform))]
        public async Task<IActionResult> GetPlatform(int id)
        {
            var platformDto = await _platformService.GetByIdAsync(id);
            if (platformDto is null)
            {
                return NotFound(new FailedResponse
                {
                    Error = PlatformResource.NotFoundSpecified,
                });
            }

            var platformSuccessfulResponse = new SuccessfulResponse<PlatformDto>();
            platformSuccessfulResponse.Items.Add(platformDto);
            platformSuccessfulResponse.Message = CommonResource.Successful;

            return Ok(platformSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Platform.GetAll, Name = nameof(GetAllPlatforms))]
        public async Task<IActionResult> GetAllPlatforms()
        {
            var platformDtos = await _platformService.GetAllAsync();
            if (!platformDtos.Any())
            {
                return NotFound(new FailedResponse
                {
                    Error = PlatformResource.NotFound,
                });
            }

            var platformSuccessfulResponse = new SuccessfulResponse<PlatformDto>();
            platformSuccessfulResponse.Items.AddRange(platformDtos);
            platformSuccessfulResponse.Message = CommonResource.Successful;

            return Ok(platformSuccessfulResponse);
        }
    }
}
