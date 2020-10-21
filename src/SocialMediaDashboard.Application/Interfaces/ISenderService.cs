using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement sender service.
    /// </summary>
    public interface ISenderService
    {
        /// <summary>
        /// Render and send mail.
        /// </summary>
        /// <typeparam name="TModel">Generic model.</typeparam>
        /// <param name="model">Model.</param>
        /// <param name="view">View.</param>
        /// <param name="recipient">Recipient.</param>
        /// <param name="subject">Subject.</param>
        Task RenderAndSendAsync<TModel>(TModel model, string view, string recipient, string subject);
    }
}
