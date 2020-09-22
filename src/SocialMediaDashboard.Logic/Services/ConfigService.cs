using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IConfigService"/>
    public class ConfigService : IConfigService
    {
        private readonly IWritableOptions<ConnectionSettings> _connectionSettings;
        private readonly IWritableOptions<JwtSettings> _jwtSettings;
        private readonly IWritableOptions<SentrySettings> _sentrySettings;
        private readonly IWritableOptions<SocialNetworksSettings> _vkSettings;

        public ConfigService(IWritableOptions<ConnectionSettings> connectionSettings,
                             IWritableOptions<JwtSettings> jwtSettings,
                             IWritableOptions<SentrySettings> sentrySettings,
                             IWritableOptions<SocialNetworksSettings> vkSettings)
        {
            _connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _sentrySettings = sentrySettings ?? throw new ArgumentNullException(nameof(sentrySettings));
            _vkSettings = vkSettings ?? throw new ArgumentNullException(nameof(vkSettings));
        }

        /// <inheritdoc/>
        public Task CheckAndUpdateConnection(string dataProvider, DataProviderType dataProviderType)
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

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CheckAndUpdateToken(string jwtValue, JwtConfigType jwtConfigType)
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
                            _jwtSettings.Update(x => x.TokenLifetime = TimeSpan.Parse(jwtValue, CultureInfo.InvariantCulture));
                        }
                        break;

                    // TODO: default:
                }
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CheckAndUpdateSentry(string sentryValue, SentryConfigType sentryConfigType)
        {
            if (!string.IsNullOrEmpty(sentryValue) && sentryValue != "string")
            {
                switch (sentryConfigType)
                {
                    case SentryConfigType.Dns:
                        {
                            _sentrySettings.Update(x => x.Dsn = sentryValue);
                        }
                        break;

                    case SentryConfigType.MinimumBreadcrumbLevel:
                        {
                            _sentrySettings.Update(x => x.MinimumBreadcrumbLevel = sentryValue);
                        }
                        break;

                    case SentryConfigType.MinimumEventLevel:
                        {
                            _sentrySettings.Update(x => x.MinimumEventLevel = sentryValue);
                        }
                        break;

                        // TODO: default:
                }
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CheckAndUpdateSocialNetworks(string tokenValue, SocialNetworkConfigType socialNetworkConfigType)
        {
            if (!string.IsNullOrEmpty(tokenValue) && tokenValue != "string")
            {
                switch (socialNetworkConfigType)
                {
                    case SocialNetworkConfigType.Vk:
                        {
                            _vkSettings.Update(x => x.VkAccessToken = tokenValue);
                        }
                        break;

                        // TODO: default:
                }
            }

            return Task.CompletedTask;
        }
    }
}
