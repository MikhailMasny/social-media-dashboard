using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Responses;
using System;
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
            var (platformDto, operationResult) = await _platformService.GetByIdAsync(id);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var platformSuccessfulResponse = new SuccessfulResponse<PlatformDto>();
            platformSuccessfulResponse.Items.Add(platformDto);
            platformSuccessfulResponse.Message = operationResult.Message;

            return Ok(platformSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Platform.GetAll, Name = nameof(GetAllPlatforms))]
        public async Task<IActionResult> GetAllPlatforms()
        {
            var (platformDtos, operationResult) = await _platformService.GetAllAsync();
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var platformSuccessfulResponse = new SuccessfulResponse<PlatformDto>();
            platformSuccessfulResponse.Items.AddRange(platformDtos);
            platformSuccessfulResponse.Message = operationResult.Message;

            return Ok(platformSuccessfulResponse);
        }
    }
}
