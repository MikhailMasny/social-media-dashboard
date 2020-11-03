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
using System.Collections.Generic;
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
        [HttpGet(ApiRoute.PlatformRoute.Get, Name = nameof(GetPlatform))]
        public async Task<IActionResult> GetPlatform(int id)
        {
            return Ok(new SuccessfulResponse<PlatformDto>
            {
                Message = CommonResource.Successful,
                Items = new List<PlatformDto>
                {
                    await _platformService.GetByIdAsync(id),
                },
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.PlatformRoute.GetAll, Name = nameof(GetAllPlatforms))]
        public async Task<IActionResult> GetAllPlatforms()
        {
            return Ok(new SuccessfulResponse<PlatformDto>
            {
                Message = CommonResource.Successful,
                Items = (await _platformService.GetAllAsync()).ToList(),
            });
        }
    }
}
