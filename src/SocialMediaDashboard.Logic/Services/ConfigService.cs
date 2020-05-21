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

        public ConfigService(IWritableOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
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

                    //default:
                }
            }
        }
    }
}
