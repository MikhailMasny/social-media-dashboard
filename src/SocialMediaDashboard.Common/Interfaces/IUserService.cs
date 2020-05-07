using SocialMediaDashboard.Common.DTO;
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
        /// <param name="password">Password.</param>
        /// <returns>Operation result, message and user data.</returns>
        Task<(bool result, string message, UserDTO user)> Authenticate(string email, string password);

        /// <summary>
        /// Registration user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="name">Name.</param>
        /// <returns>Operation result, message and user data.</returns>
        Task<(bool result, string message, UserDTO user)> Registration(string email, string password, string name);

        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="name">Name.</param>
        /// <param name="avatar">Avatar.</param>
        /// <returns>Operation result, message and user data.</returns>
        Task<(bool result, string message, UserDTO user)> UpdateProfile(string email, string name, string avatar);
    }
}
