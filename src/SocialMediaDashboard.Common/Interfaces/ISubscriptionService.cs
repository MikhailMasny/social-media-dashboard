using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement subscription service.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Create subscription.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="account">User account.</param>
        /// <param name="accountType">Account type.</param>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        Task<bool> AddSubscriptionAsync(string userId, int accountId, string account, AccountType accountType, SubscriptionType subscriptionType);

        /// <summary>
        /// Get all subscription by user identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of subscription data transfet objects.</returns>
        Task<IEnumerable<SubscriptionDto>> GetAllUserSubscriptionsAsync(string userId);

        /// <summary>
        /// Get all subscription by account type.
        /// </summary>
        /// <param name="accountType">Account type.</param>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>List of subscription data transfet objects.</returns>
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByTypeAsync(AccountType accountType, SubscriptionType subscriptionType);

        /// <summary>
        /// Delete selected subscription.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> DeleteSubscriptionAsync(int id, string userId);

        /// <summary>
        /// Check subscription.
        /// </summary>
        /// <param name="id">Subscription identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> SubscriptionExistAsync(int id);
    }
}
