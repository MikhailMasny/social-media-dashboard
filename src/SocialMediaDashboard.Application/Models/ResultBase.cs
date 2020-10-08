using System.Collections.Generic;

namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Result (base object).
    /// </summary>
    public abstract class ResultBase
    {
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
