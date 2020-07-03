using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Media create request.
    /// </summary>
    public class MediaCreateRequest
    {
        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Account type.
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Subscription type.
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }
    }
}
