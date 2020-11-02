using SocialMediaDashboard.Application.Models;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement profile service.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Create profile (for internal use).
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="name">Full name.</param>
        Task CreateAsync(string userId, string name);

        /// <summary>
        /// Get profile by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Profile data transfer object.</returns>
        Task<ProfileDto> GetByUserIdAsync(string userId);

        /// <summary>
        /// Update profile by user identifier.
        /// </summary>
        /// <param name="profileDto">Profile data transfer object.</param>
        /// <returns>Profile data transfer object.</returns>
        Task<ProfileDto> UpdateAsync(ProfileDto profileDto);
    }
}
