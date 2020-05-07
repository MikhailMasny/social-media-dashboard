using SocialMediaDashboard.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <returns>User data (DTO).</returns>
        Task<UserDTO> Authenticate(string email, string password);

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Users.</returns>
        IEnumerable<UserDTO> GetAll();
    }
}
