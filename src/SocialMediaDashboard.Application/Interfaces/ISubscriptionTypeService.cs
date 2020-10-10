using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement subscription type service.
    /// </summary>
    public interface ISubscriptionTypeService
    {
        /// <summary>
        /// Get all subscription type data transfer objects.
        /// </summary>
        /// <returns>List of subscription type data transfer objects.</returns>
        Task<IEnumerable<SubscriptionTypeDto>> GetAllAsync();

        /// <summary>
        /// Get subscription type data transfer object by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Subscription type data transfer object.</returns>
        Task<SubscriptionTypeDto> GetByIdAsync(int id);

        /// <summary>
        /// Check subscription type.
        /// </summary>
        /// <param name="id">Subscription identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> SubscriptionTypeExistAsync(int id);
    }
}
