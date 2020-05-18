using SocialMediaDashboard.Common.DTO;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> LoginAsync(string email, string password);

        /// <summary>
        /// Sign up (create new user).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> RegistrationAsync(string email, string password);
    }
}
