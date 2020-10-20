using System.Collections.Generic;

namespace SocialMediaDashboard.Web.Contracts.Responses
{
    /// <summary>
    /// Successful response.
    /// </summary>
    public class SuccessfulResponse<T>
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data transfer objects.
        /// </summary>
        public List<T> Items { get; } = new List<T>();
    }
}
