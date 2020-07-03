using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement media service.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>List of media data transfet objects.</returns>
        Task<IEnumerable<MediaDto>> GetAllAccounts();

        /// <summary>
        /// Add user media account.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Media account.</param>
        /// <returns>Media data transfet object.</returns>
        Task<MediaDto> AddUserAccount(string userId, string account);
    }
}
