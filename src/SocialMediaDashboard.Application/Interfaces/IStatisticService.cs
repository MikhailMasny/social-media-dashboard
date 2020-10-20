using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement statistic service.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Get followers data from Vk accounts.
        /// </summary>
        Task GetFollowersFromVkAsync();

        /// <summary>
        /// Add followers data from Instagram accounts.
        /// </summary>
        Task GetFollowersFromInstagramAsync();

        /// <summary>
        /// Add subscribers data from YouTube channel.
        /// </summary>
        Task GetSubscribersFromYouTubeAsync();
    }
}
