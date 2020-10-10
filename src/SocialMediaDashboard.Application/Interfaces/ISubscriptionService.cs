using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Enums;
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
        /// <param name="userId">User identifier.</param>
        /// <param name="id">Identifier.</param>
        /// <returns>Subscription data transfet object with operation result.</returns>
        Task<(SubscriptionDto subscriptionDto, SubscriptionResult subscriptionResult)> GetSubscriptionByIdAsync(string userId, int id);

        /// <summary>
        /// Delete subscription by identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="id">Identifier.</param>
        /// <returns>Operation result.</returns>
        Task<SubscriptionResult> DeleteSubscriptionByIdAsync(string userId, int id);




        //    /// <summary>
        //    /// Get account by user identifier.
        //    /// </summary>
        //    /// <param name="userId">User identifier.</param>
        //    /// <param name="accountId">Account identifier.</param>
        //    /// <returns>Account data transfet objects with operation result.</returns>
        //    Task<(AccountDto accountDto, AccountResult accountResult)> GetAccountByUserIdAsync(string userId, int accountId);


        ///// <summary>
        ///// Create subscription.
        ///// </summary>
        ///// <param name="userId">User identifier.</param>
        ///// <param name="accountId">Account identifier.</param>
        ///// <param name="account">User account.</param>
        ///// <param name="accountType">Account type.</param>
        ///// <param name="subscriptionType">Subscription type.</param>
        ///// <returns>Operation result.</returns>
        //Task<bool> AddSubscriptionAsync(string userId, int accountId, string account, AccountKind accountType, SubscriptionKind subscriptionType);

        ///// <summary>
        ///// Get all subscription by user identifier.
        ///// </summary>
        ///// <param name="userId">User identifier.</param>
        ///// <returns>List of subscription data transfet objects.</returns>
        //Task<IEnumerable<SubscriptionDto>> GetAllUserSubscriptionsAsync(string userId);

        ///// <summary>
        ///// Get all subscription by account type.
        ///// </summary>
        ///// <param name="accountType">Account type.</param>
        ///// <param name="subscriptionType">Subscription type.</param>
        ///// <returns>List of subscription data transfet objects.</returns>
        //Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByTypeAsync(AccountKind accountType, SubscriptionKind subscriptionType);

        ///// <summary>
        ///// Delete selected subscription.
        ///// </summary>
        ///// <param name="id">Identifier.</param>
        ///// <param name="userId">User identifier.</param>
        ///// <returns>Operation result.</returns>
        //Task<bool> DeleteSubscriptionAsync(int id, string userId);

        ///// <summary>
        ///// Check subscription.
        ///// </summary>
        ///// <param name="id">Subscription identifier.</param>
        ///// <returns>Operation result.</returns>
        //Task<bool> SubscriptionExistAsync(int id);
    }
}
