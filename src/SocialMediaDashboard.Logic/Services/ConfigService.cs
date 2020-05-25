using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using System;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IConfigService"/>
    public class ConfigService : IConfigService
    {
        private readonly IWritableOptions<ConnectionSettings> _connectionSettings;
        private readonly IWritableOptions<JwtSettings> _jwtSettings;

        public ConfigService(IWritableOptions<ConnectionSettings> connectionSettings,
                             IWritableOptions<JwtSettings> jwtSettings)
        {
            _connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        /// <inheritdoc/>
        public void CheckAndUpdateConnection(string dataProvider, DataProviderType dataProviderType)
        {
            if (!string.IsNullOrEmpty(dataProvider) && dataProvider != "string")
            {
                switch (dataProviderType)
                {
                    case DataProviderType.MSSQL:
                        {
                            _connectionSettings.Update(x => x.MSSQLConnection = dataProvider);
                        }
                        break;

                    case DataProviderType.Docker:
                        {
                            _connectionSettings.Update(x => x.DockerConnection = dataProvider);
                        }
                        break;

                    case DataProviderType.SQLite:
                        {
                            _connectionSettings.Update(x => x.SQLiteConnection = dataProvider);
                        }
                        break;

                    case DataProviderType.PostgreSQL:
                        {
                            _connectionSettings.Update(x => x.PostgreSQLConnection = dataProvider);
                        }
                        break;

                    // TODO: default:
                }
            }
        }

        /// <inheritdoc/>
        public void CheckAndUpdateToken(string jwtValue, JwtConfigType jwtConfigType)
        {
            if (!string.IsNullOrEmpty(jwtValue) && jwtValue != "string")
            {
                switch (jwtConfigType)
                {
                    case JwtConfigType.Secret:
                        {
                            _jwtSettings.Update(x => x.Secret = jwtValue);
                        }
                        break;

                    case JwtConfigType.TokenLifetime:
                        {
                            _jwtSettings.Update(x => x.TokenLifetime = TimeSpan.Parse(jwtValue));
                        }
                        break;

                    // TODO: default:
                }
            }
        }
    }
}
