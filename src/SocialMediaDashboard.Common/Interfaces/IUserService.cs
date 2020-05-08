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
        /// <returns>Auth data transfer object.</returns>
        Task<AuthDTO> Authenticate(string email, string password);

        /// <summary>
        /// Registration user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="name">Name.</param>
        /// <returns>Auth data transfer object.</returns>
        Task<AuthDTO> Registration(string email, string password, string name);

        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="name">Name.</param>
        /// <param name="avatar">Avatar.</param>
        /// <returns>Auth data transfer object.</returns>
        Task<AuthDTO> UpdateProfile(string email, string name, string avatar);
    }
}
