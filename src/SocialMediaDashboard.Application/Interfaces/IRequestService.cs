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
        /// Get data by channel from YouTube api.
        /// </summary>
        /// <param name="channel">YouTube channel.</param>
        /// <returns>YouTube data.</returns>
        Task<YouTubeResult> GetDataByChannelFromYouTubeApiAsync(string channel);
    }
}
