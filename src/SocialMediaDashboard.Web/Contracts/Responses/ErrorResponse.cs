using SocialMediaDashboard.Web.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.Web.Contracts.Responses
{
    /// <summary>
    /// Error validation response.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Errors.
        /// </summary>
        public List<ValidationErrorModel> Errors { get; } = new List<ValidationErrorModel>();
    }
}
