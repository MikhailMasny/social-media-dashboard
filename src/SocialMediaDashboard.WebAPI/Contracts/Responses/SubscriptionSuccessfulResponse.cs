using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Subscription successful response.
    /// </summary>
    public class SubscriptionSuccessfulResponse
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Subscriptions.
        /// </summary>
        public List<SubscriptionDto> Subscriptions { get; } = new List<SubscriptionDto>();
    }
}
