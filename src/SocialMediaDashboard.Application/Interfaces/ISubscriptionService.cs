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
        /// <returns>Subscription data transfet object.</returns>
        Task<SubscriptionDto> CreateAsync(string userId, string accountName, int subscriptionTypeId);

        /// <summary>
        /// Get subscription by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Subscription data transfet object.</returns>
        Task<SubscriptionDto> GetByIdAsync(int id, string userId);

        /// <summary>
        /// Get all subscriptions.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of subscription data transfet objects.</returns>
        Task<IEnumerable<SubscriptionDto>> GetAllAsync(string userId);

        /// <summary>
        /// Update subscription.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="subscriptionTypeId">Subscription type identifier.</param>
        /// <returns>Subscription data transfet object.</returns>
        Task<SubscriptionDto> UpdateAsync(int id, string userId, string accountName, int subscriptionTypeId);

        /// <summary>
        /// Delete subscription by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="userId">User identifier.</param>
        Task DeleteByIdAsync(int id, string userId);

        /// <summary>
        /// Get account names by subscription type identifier.
        /// </summary>
        /// <param name="subscriptionTypeId">Subscription type identifier.</param>
        /// <returns>List of subscription data transfet objects.</returns>
        Task<IEnumerable<SubscriptionDto>> GetAccountNamesBySubscriptionTypeIdAsync(int subscriptionTypeId);
    }
}
