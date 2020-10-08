using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement Instagram service.
    /// </summary>
    public interface IInstagramService
    {
        /// <summary>
        /// Get user followers by user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Count of followers.</returns>
        Task<int> GetFollowersByUserNameAsync(string userName);
    }
}
