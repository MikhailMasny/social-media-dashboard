using SocialMediaDashboard.Domain.Enums;

namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Subscription create request.
    /// </summary>
    public class SubscriptionCreateRequest
    {
        /// <summary>
        /// Account name.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Subscription type identifier.
        /// </summary>
        public int SubscriptionTypeId { get; set; }
    }
}
