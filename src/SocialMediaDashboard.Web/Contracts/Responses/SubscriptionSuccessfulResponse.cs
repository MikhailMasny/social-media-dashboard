using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.Web.Contracts.Responses
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
        /// Subscription data transfer objects.
        /// </summary>
        public List<SubscriptionDto> Subscriptions { get; } = new List<SubscriptionDto>();
    }
}
