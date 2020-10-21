using System.Threading.Tasks;

namespace SocialMediaDashboard.Application.Interfaces
{
    /// <summary>
    /// Interface for implement email service.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="recipient">Recipient.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="body">Body.</param>
        Task SendMessageAsync(string recipient, string subject, string body);
    }
}
