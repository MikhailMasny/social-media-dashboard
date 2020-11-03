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
        /// Sign up (create new user).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="name">Name.</param>
        /// <returns>Email confirmation token for the specified user.</returns>
        Task<string> SignUpAsync(string email, string password, string name);

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Authentication data transfer object.</returns>
        Task<AuthenticationDto> SignInAsync(string email, string password);

        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="code">Verify code.</param>
        /// <returns>Authentication data transfer object.</returns>
        Task<AuthenticationDto> ConfirmEmailAsync(string email, string code);

        /// <summary>
        /// Get user by Email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserDto> GetUserByEmailAsync(string email);

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserDto> GetUserByIdAsync(string id);

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>User data transfet object.</returns>
        Task<UserDto> GetUserByNameAsync(string username);

        /// <summary>
        /// Restore password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Confirmation data transfer object.</returns>
        Task<ConfirmationDto> RestorePasswordAsync(string email);

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="newPassword">New password.</param>
        /// <param name="code">Verify code.</param>
        /// <returns>Profile name and authentication data transfer object.</returns>
        Task<(string name, AuthenticationDto authenticationDto)> ResetPasswordAsync(string email, string newPassword, string code);

        /// <summary>
        /// Refresh user token.
        /// </summary>
        /// <param name="token">JWT Token.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <returns>Authentication data transfer object.</returns>
        Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken);
    }
}
