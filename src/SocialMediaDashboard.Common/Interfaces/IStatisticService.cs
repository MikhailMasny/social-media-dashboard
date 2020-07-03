using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement statistic service.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Add statistics data from Vk accounts.
        /// </summary>
        Task AddDataByVkAccounts();
    }
}
