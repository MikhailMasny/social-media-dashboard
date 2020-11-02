using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement platform service.
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// Get all platform data transfer objects.
        /// </summary>
        /// <returns>List of platform data transfer objects.</returns>
        Task<IEnumerable<PlatformDto>> GetAllAsync();

        /// <summary>
        /// Get platform data transfer object by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Platform data transfer object.</returns>
        Task<PlatformDto> GetByIdAsync(int id);
    }
}
