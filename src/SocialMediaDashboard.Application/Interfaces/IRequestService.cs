using SocialMediaDashboard.Application.Models;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement request service.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Get data by channel from YouTube Api.
        /// </summary>
        /// <param name="channel">YouTube channel.</param>
        /// <returns>YouTube data.</returns>
        Task<YouTubeResult> GetDataFromYouTubeApiByChannelAsync(string channel);

        /// <summary>
        /// Get data by username from YouTube Api.
        /// </summary>
        /// <param name="username">YouTube username.</param>
        /// <returns>YouTube data.</returns>
        Task<YouTubeResult> GetDataFromYouTubeApiByUsernameAsync(string username);

        /// <summary>
        /// Get data by video from YouTube Api.
        /// </summary>
        /// <param name="video">Video link.</param>
        /// <returns>YouTube data.</returns>
        Task<YouTubeResult> GetDataFromYouTubeApiByVideoAsync(string video);
    }
}
