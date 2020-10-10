using SocialMediaDashboard.Application.Models;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="code">Verify code.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> ConfirmEmailAsync(string email, string code);

        /// <summary>
        /// Get user by Email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserResult> GetUserByEmailAsync(string email);

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserResult> GetUserByIdAsync(string id);

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserResult> GetUserByNameAsync(string username);

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> SignInAsync(string email, string password);

        /// <summary>
        /// Restore password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Confirmation result data transfer object.</returns>
        Task<ConfirmationResult> RestorePasswordAsync(string email);

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="newPassword">New password.</param>
        /// <param name="code">Verify code.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> ResetPasswordAsync(string email, string newPassword, string code);

        /// <summary>
        /// Sign up (create new user).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="userName">User nickname.</param>
        /// <param name="password">Password.</param>
        /// <returns>Confirmation result data transfer object.</returns>
        Task<ConfirmationResult> RegistrationAsync(string email, string userName, string password);

        /// <summary>
        /// Refresh user token.
        /// </summary>
        /// <param name="token">JWT Token.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <returns>Authentication result data transfer object.</returns>
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
