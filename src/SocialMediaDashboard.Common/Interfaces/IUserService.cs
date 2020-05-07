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
        /// <returns>Message and user data.</returns>
        Task<(bool result, string message, UserDTO user)> Authenticate(string email, string password);

        /// <summary>
        /// Registration user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="name">Name.</param>
        /// <returns>Message and token.</returns>
        Task<(bool result, string message, UserDTO user)> Registration(string email, string password, string name);
    }
}
