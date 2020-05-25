using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using System;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        [HttpPost(ApiRoutes.Config.Connection)]
        public IActionResult UpdateConnections([FromBody] ConnectionSettingsRequest request)
        {
            _configService.CheckAndUpdateConnection(request.MSSQLConnection, DataProviderType.MSSQL);
            _configService.CheckAndUpdateConnection(request.DockerConnection, DataProviderType.Docker);
            _configService.CheckAndUpdateConnection(request.SQLiteConnection, DataProviderType.SQLite);
            _configService.CheckAndUpdateConnection(request.PostgreSQLConnection, DataProviderType.PostgreSQL);

            return Ok();
        }

        [HttpPost(ApiRoutes.Config.Token)]
        public IActionResult UpdateToken([FromBody] JwtSettingsRequest request)
        {
            _configService.CheckAndUpdateToken(request.Secret, JwtConfigType.Secret);
            _configService.CheckAndUpdateToken(request.TokenLifetime, JwtConfigType.TokenLifetime);

            return Ok();
        }
    }
}
