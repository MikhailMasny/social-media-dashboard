using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement observation service.
    /// </summary>
    public interface IObservationService
    {
        /// <summary>
        /// Get all observation data transfer objects.
        /// </summary>
        /// <returns>List of observation data transfer objects.</returns>
        Task<IEnumerable<ObservationDto>> GetAllAsync();

        /// <summary>
        /// Get observation data transfer object by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Observation data transfer object.</returns>
        Task<ObservationDto> GetByIdAsync(int id);
    }
}
