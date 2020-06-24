using SocialMediaDashboard.WebAPI.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
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
