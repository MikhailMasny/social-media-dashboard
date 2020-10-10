using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Domain.Enums;
using SocialMediaDashboard.Domain.Helpers;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
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

        public Task CheckAndUpdateConnection(string dataProvider, DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                case DataProviderType.MsSqlServer:
                    {
                        _connectionSettings.Update(x => x.MsSqlServerConnection = dataProvider);
                    }
                    break;

                case DataProviderType.PostgreSql:
                    {
                        _connectionSettings.Update(x => x.PostgreSqlConnection = dataProvider);
                    }
                    break;

                    // TODO: default:
            }

            return Task.CompletedTask;
        }

        public Task CheckAndUpdateToken(string jwtValue, JwtConfigType jwtConfigType)
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

            return Task.CompletedTask;
        }

        public Task CheckAndUpdateSentry(string sentryValue, SentryConfigType sentryConfigType)
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

            return Task.CompletedTask;
        }

        public Task CheckAndUpdateSocialNetworks(string tokenValue, SocialNetworkConfigType socialNetworkConfigType)
        {
            switch (socialNetworkConfigType)
            {
                case SocialNetworkConfigType.VkAccessToken:
                    {
                        _vkSettings.Update(x => x.VkAccessToken = tokenValue);
                    }
                    break;

                case SocialNetworkConfigType.InstagramUsername:
                    {
                        _vkSettings.Update(x => x.InstagramAccount.Username = tokenValue);
                    }
                    break;

                case SocialNetworkConfigType.InstagramPassword:
                    {
                        _vkSettings.Update(x => x.InstagramAccount.Password = tokenValue);
                    }
                    break;

                case SocialNetworkConfigType.YouTubeAccessToken:
                    {
                        _vkSettings.Update(x => x.YouTubeAccessToken = tokenValue);
                    }
                    break;

                    // TODO: default:
            }

            return Task.CompletedTask;
        }
    }
}
