using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement account service.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of account data transfet objects.</returns>
        Task<IEnumerable<AccountDto>> GetAllUserAccountsAsync(string userId);

        /// <summary>
        /// Get account by identifier.
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <returns>Account data transfet objects.</returns>
        Task<AccountDto> GetAccountAsync(int id);

        /// <summary>
        /// Add user social media account.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Account account.</param>
        /// <param name="accountType">Account type.</param>
        /// <returns>Operation result.</returns>
        Task<bool> AddAccountAsync(string userId, string account, AccountType accountType);

        /// <summary>
        /// Check account.
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> AccountExistAsync(int id);
    }
}
