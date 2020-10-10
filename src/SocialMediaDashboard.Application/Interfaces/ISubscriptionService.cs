using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
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
        /// <param name="accountName">Account name.</param>
        /// <param name="subscriptionTypeId">Subscription type identifier.</param>
        /// <returns>Subscription data transfet object with operation result.</returns>
        Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> CreateSubscriptionAsync(string userId, string accountName, int subscriptionTypeId);

        /// <summary>
        /// Get subscription by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Subscription data transfet object with operation result.</returns>
        Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> GetSubscriptionByIdAsync(int id, string userId);

        /// <summary>
        /// Get all subscriptions.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of subscription data transfet objects with operation result.</returns>
        Task<(IEnumerable<SubscriptionDto> subscriptionDto, SubscriptionResult subscriptionResult)> GetAllSubscriptionAsync(string userId);

        /// <summary>
        /// Update subscription.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="subscriptionTypeId">Subscription type identifier.</param>
        /// <returns>Subscription data transfet object with operation result.</returns>
        Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> UpdateSubscriptionAsync(int id, string userId, string accountName, int subscriptionTypeId);

        /// <summary>
        /// Delete subscription by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Operation result.</returns>
        Task<SubscriptionResult> DeleteSubscriptionByIdAsync(int id, string userId);
    }
}
