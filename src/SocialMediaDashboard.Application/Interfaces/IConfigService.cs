using SocialMediaDashboard.Domain.Enums;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
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
        Task CheckAndUpdateConnection(string dataProvider, DataProviderType dataProviderType);

        /// <summary>
        /// Check and update JWT settings.
        /// </summary>
        /// <param name="jwtValue">JWT key value.</param>
        /// <param name="jwtConfigType">Type of JWT key value.</param>
        Task CheckAndUpdateToken(string jwtValue, JwtConfigType jwtConfigType);

        /// <summary>
        /// Check and update Sentry settings.
        /// </summary>
        /// <param name="sentryValue">Sentry key value.</param>
        /// <param name="sentryConfigType">Type of Sentry key value.</param>
        Task CheckAndUpdateSentry(string sentryValue, SentryConfigType sentryConfigType);

        /// <summary>
        /// Check and update social networks settings.
        /// </summary>
        /// <param name="tokenValue">Token key value.</param>
        /// <param name="socialNetworkConfigType">Type of social network key value.</param>
        Task CheckAndUpdateSocialNetworks(string tokenValue, SocialNetworkConfigType socialNetworkConfigType);
    }
}
