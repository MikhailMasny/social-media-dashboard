using System.Collections.Generic;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Result (base object).
    /// </summary>
    public abstract class BaseResult
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
