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
    }
}
