using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
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
        Task<string> GetSubscribersByChannelAsync(string channel);

        // TODO: change it to int
    }
}
