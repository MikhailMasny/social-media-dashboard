using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement media service.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>List of media data transfet objects.</returns>
        Task<IEnumerable<MediaDto>> GetAllAccounts();

        /// <summary>
        /// Get account by media identifier.
        /// </summary>
        /// <param name="id">Media identifier.</param>
        /// <returns>Media data transfet objects.</returns>
        Task<MediaDto> GetAccount(int id);

        /// <summary>
        /// Add user media account.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="account">Media account.</param>
        /// <param name="accountType">Account type.</param>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result</returns>
        Task<bool> AddUserAccount(string userId, string account, AccountType accountType, SubscriptionType subscriptionType);
    }
}
