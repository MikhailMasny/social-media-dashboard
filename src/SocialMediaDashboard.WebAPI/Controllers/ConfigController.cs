using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Constants;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.WebAPI.Contracts.Requests;
using System;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }

        [HttpPost(ApiRoutes.Config.Connection)]
        public IActionResult Test([FromBody] ConnectionStringsRequest request)
        {
            _configService.CheckAndUpdateConnection(request.MSSQLConnection, DataProviderType.MSSQL);
            _configService.CheckAndUpdateConnection(request.DockerConnection, DataProviderType.Docker);
            _configService.CheckAndUpdateConnection(request.SQLiteConnection, DataProviderType.SQLite);
            _configService.CheckAndUpdateConnection(request.PostgreSQLConnection, DataProviderType.PostgreSQL);

            return Ok();
        }
    }
}
