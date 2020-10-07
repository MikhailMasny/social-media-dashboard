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
        /// Get account by identifier (use by internal services of the application).
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <returns>Account data transfet objects.</returns>
        Task<AccountDto> GetAccountAsync(int id);

        /// <summary>
        /// Check account.
        /// </summary>
        /// <param name="id">Account identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Account operation result.</returns>
        Task<AccountResult> ValidateAccountExistAsync(int id, string userId);

        /// <summary>
        /// Add user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Account account.</param>
        /// <param name="accountType">Account type.</param>
        /// <returns>Account data transfet objects with operation result.</returns>
        Task<(AccountDto accountDto, AccountResult accountResult)> CreateAccountByUserIdAsync(string userId, string account, AccountType accountType);

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
        /// <returns>List of account data transfet objects with operation result.</returns>
        Task<(IEnumerable<AccountDto> accountDtos, AccountResult accountResult)> GetAllUserAccountsAsync(string userId);

        /// <summary>
        /// Update user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier account.</param>
        /// <param name="accountName">Account account.</param>
        /// <param name="accountType">Account type.</param>
        /// <returns>Account operation result.</returns>
        Task<AccountResult> UpdateAccountByUserIdAsync(string userId, int accountId, string accountName, AccountType accountType);

        /// <summary>
        /// Delete user social media account by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Account operation result.</returns>
        Task<AccountResult> DeleteAccountByUserIdAsync(string userId, int accountId);
    }
}
