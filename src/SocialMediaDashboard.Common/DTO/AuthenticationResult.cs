using System.Collections.Generic;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Authentication result.
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Result of operation.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Errors.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
