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
        /// Confirm email.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="code">Verify code.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> ConfirmEmailAsync(string userId, string code);

        /// <summary>
        /// Get user Id.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>Id.</returns>
        Task<string> GetUserIdByNameAsync(string userName);

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
        /// <returns>Email confirmation result data transfer object.</returns>
        Task<RegistrationResult> RegistrationAsync(string email, string password);
    }
}
