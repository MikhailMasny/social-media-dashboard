using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Authentication failed response.
    /// </summary>
    public class FailedResponse
    {
        /// <summary>
        /// Errors.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
