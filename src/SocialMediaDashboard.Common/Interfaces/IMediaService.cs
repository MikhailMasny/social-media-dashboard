using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement media service.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Add user media account.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Media account.</param>
        Task AddUserAccount(string userId, string account);
    }
}
