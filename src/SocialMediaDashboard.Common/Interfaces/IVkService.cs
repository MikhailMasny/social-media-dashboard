using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement Vk service.
    /// </summary>
    public interface IVkService
    {
        /// <summary>
        /// Get user followers by user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Count of followers.</returns>
        Task<int?> GetFollowers(string userName);

        /// <summary>
        /// Get user followers by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Count of followers.</returns>
        Task<int?> GetFollowers(long userId);
    }
}
