using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Web.Constants;
using SocialMediaDashboard.Web.Contracts.Requests;
using SocialMediaDashboard.Web.Contracts.Responses;
using SocialMediaDashboard.Web.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(ApiRoute.Profile.Get, Name = nameof(GetProfile))]
        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.GetUserId();
            var (profileDto, operationResult) = await _profileService.GetByUserIdAsync(userId);
            if (!operationResult.Result)
            {
                return NotFound(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var profileSuccessfulResponse = new SuccessfulResponse<ProfileDto>();
            profileSuccessfulResponse.Items.Add(profileDto);
            profileSuccessfulResponse.Message = operationResult.Message;

            return Ok(profileSuccessfulResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut(ApiRoute.Profile.Update, Name = nameof(UpdateProfile))]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileUpdateRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            static byte[] ConvertAvatar(IFormFile avatar)
            {
                byte[] userProfileImage = null;
                if (avatar != null)
                {
                    using var binaryReader = new BinaryReader(avatar.OpenReadStream());
                    userProfileImage = binaryReader.ReadBytes((int)avatar.Length);
                }

                return userProfileImage;
            }

            var profileDto = new ProfileDto
            {
                UserId = HttpContext.GetUserId(),
                Name = request.Name,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Country = request.Country,
                Avatar = ConvertAvatar(request.Avatar),
            };

            var (updatedProfileDto, operationResult) = await _profileService.UpdateAsync(profileDto);
            if (!operationResult.Result)
            {
                return Conflict(new FailedResponse
                {
                    Error = operationResult.Message,
                });
            }

            var profileSuccessfulResponse = new SuccessfulResponse<ProfileDto>();
            profileSuccessfulResponse.Items.Add(updatedProfileDto);
            profileSuccessfulResponse.Message = operationResult.Message;

            return Ok(profileSuccessfulResponse);
        }
    }
}
