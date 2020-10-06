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
        /// Check account.
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> AccountExistAsync(int id);

        /// <summary>
        /// Get account by identifier.
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <returns>Account data transfet objects.</returns>
        Task<AccountDto> GetAccountAsync(int id);

        /// <summary>
        /// Get account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Account data transfet objects with operation result.</returns>
        Task<(AccountDto accountDto, AccountResult accountResult)> GetAccountByUserIdAsync(string userId, int accountId);

        /// <summary>
        /// Get all accounts by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of account data transfet objects.</returns>
        Task<IEnumerable<AccountDto>> GetAllUserAccountsAsync(string userId);

        /// <summary>
        /// Add user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Account account.</param>
        /// <param name="accountType">Account type.</param>
        /// <returns>Operation result.</returns>
        Task<bool> AddAccountByUserIdAsync(string userId, string account, AccountType accountType);

        /// <summary>
        /// Delete user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Account operation result.</returns>
        Task<AccountResult> DeleteAccountByUserIdAsync(string userId, int accountId);

        /// <summary>
        /// Update user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier account.</param>
        /// <param name="accountName">Account account.</param>
        /// <param name="accountType">Account type.</param>
        /// <returns>Account operation result.</returns>
        Task<AccountResult> UpdateAccountByUserIdAsync(string userId, int accountId, string accountName, AccountType accountType);
    }
}
