using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement config service.
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Check and update data provider.
        /// </summary>
        /// <param name="dataProvider">Data provider.</param>
        /// <param name="dataProviderType">Type of data provider.</param>
        void CheckAndUpdateConnection(string dataProvider, DataProviderType dataProviderType);

        /// <summary>
        /// Check and update JWT settings.
        /// </summary>
        /// <param name="jwtValue">JWT key value.</param>
        /// <param name="jwtConfigType">Type of JWT key value.</param>
        void CheckAndUpdateToken(string jwtValue, JwtConfigType jwtConfigType);

        /// <summary>
        /// Check and update Sentry settings.
        /// </summary>
        /// <param name="sentryValue">Sentry key value.</param>
        /// <param name="sentryConfigType">Type of Sentry key value.</param>
        void CheckAndUpdateSentry(string sentryValue, SentryConfigType sentryConfigType);
    }
}
