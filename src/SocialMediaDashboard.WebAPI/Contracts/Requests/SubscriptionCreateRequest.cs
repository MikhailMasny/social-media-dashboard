using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Subscription create request.
    /// </summary>
    public class SubscriptionCreateRequest
    {
        /// <summary>
        /// Media identifier.
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Subscription type.
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }
    }
}
