using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement render service.
    /// </summary>
    public interface IRenderService
    {
        /// <summary>
        /// Generate HTML document.
        /// </summary>
        /// <typeparam name="TModel">Generic model.</typeparam>
        /// <param name="viewName">View name.</param>
        /// <param name="model">Model.</param>
        /// <returns>The string representation of the HMTL document.</returns>
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
