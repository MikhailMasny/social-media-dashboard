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
        /// Get all subscription by media type.
        /// </summary>
        /// <param name="accountType">Account type.</param>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>List of subscription data transfet objects.</returns>
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByType(AccountType accountType, SubscriptionType subscriptionType);
    }
}
