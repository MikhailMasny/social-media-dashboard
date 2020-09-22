using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(ApiRoutes.Config.Connection, Name = nameof(UpdateConnections))]
        public async Task<IActionResult> UpdateConnections([FromBody] ConnectionSettingsRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            await _configService.CheckAndUpdateConnection(request.MSSQLConnection, DataProviderType.MSSQL);
            await _configService.CheckAndUpdateConnection(request.DockerConnection, DataProviderType.Docker);
            await _configService.CheckAndUpdateConnection(request.SQLiteConnection, DataProviderType.SQLite);
            await _configService.CheckAndUpdateConnection(request.PostgreSQLConnection, DataProviderType.PostgreSQL);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(ApiRoutes.Config.Token, Name = nameof(UpdateToken))]
        public async Task<IActionResult> UpdateToken([FromBody] JwtSettingsRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            await _configService.CheckAndUpdateToken(request.Secret, JwtConfigType.Secret);
            await _configService.CheckAndUpdateToken(request.TokenLifetime, JwtConfigType.TokenLifetime);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(ApiRoutes.Config.Sentry, Name = nameof(UpdateSentry))]
        public async Task<IActionResult> UpdateSentry([FromBody] SentrySettingsRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            await _configService.CheckAndUpdateSentry(request.Dsn, SentryConfigType.Dns);
            await _configService.CheckAndUpdateSentry(request.MinimumBreadcrumbLevel, SentryConfigType.MinimumBreadcrumbLevel);
            await _configService.CheckAndUpdateSentry(request.MinimumEventLevel, SentryConfigType.MinimumEventLevel);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(ApiRoutes.Config.SocialNetworks, Name = nameof(UpdateSocialNetworks))]
        public async Task<IActionResult> UpdateSocialNetworks([FromBody] SocialNetworksSettingsRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            await _configService.CheckAndUpdateSocialNetworks(request.VkAccessToken, SocialNetworkConfigType.Vk);

            return NoContent();
        }
    }
}
