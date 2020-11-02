using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Enums;
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
        Task<bool> IsExistAsync(int id);

        /// <summary>
        /// Get subscription type identifier by parameters.
        /// </summary>
        /// <param name="platformType">Platform type.</param>
        /// <param name="observationType">Observation type.</param>
        /// <returns>Subscription type identifier.</returns>
        Task<int> GetByParametersAsync(PlatformType platformType, ObservationType observationType);
    }
}
