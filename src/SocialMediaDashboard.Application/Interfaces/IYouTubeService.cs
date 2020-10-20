using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement YouTube service.
    /// </summary>
    public interface IYouTubeService
    {
        /// <summary>
        /// Get user subscribers by channel.
        /// </summary>
        /// <param name="channel">Channel.</param>
        /// <returns>Count of subscribers.</returns>
        Task<int> GetSubscribersByChannelAsync(string channel);
    }
}
