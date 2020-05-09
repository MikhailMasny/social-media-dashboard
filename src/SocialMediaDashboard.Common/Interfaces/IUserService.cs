using SocialMediaDashboard.Common.DTO;
using System.Security.Claims;
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
        /// <returns>Response data transfer object.</returns>
        Task<ResponseDTO> Authenticate(string email, string password);

        /// <summary>
        /// Registration user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="name">Name.</param>
        /// <returns>Response data transfer object.</returns>
        Task<ResponseDTO> Registration(string email, string password, string name);

        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="tokenData">Token data transfer object.</param>
        /// <param name="name">Name.</param>
        /// <param name="avatar">Avatar.</param>
        /// <returns>Response data transfer object.</returns>
        Task<ResponseDTO> UpdateProfile(TokenDTO tokenData, string name, string avatar);

        /// <summary>
        /// Get user profile.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Response data transfer object.</returns>
        Task<ResponseDTO> GetProfile(int userId);

        /// <summary>
        /// Get user data from Token.
        /// </summary>
        /// <param name="claimsPrincipal">User claims.</param>
        /// <returns>Token data transfer object.</returns>
        TokenDTO GetUserData(ClaimsPrincipal claimsPrincipal);
    }
}
