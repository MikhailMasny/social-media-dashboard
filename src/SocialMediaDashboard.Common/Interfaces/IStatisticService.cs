using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement statistic service.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Add followers data from Vk accounts.
        /// </summary>
        Task AddFollowersFromVk();
    }
}
