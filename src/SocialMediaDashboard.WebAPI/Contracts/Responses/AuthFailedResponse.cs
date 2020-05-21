using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Authentication failed response.
    /// </summary>
    public class AuthFailedResponse
    {
        /// <summary>
        /// Errors.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
