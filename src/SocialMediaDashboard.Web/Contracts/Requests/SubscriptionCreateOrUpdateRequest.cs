namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Subscription create request.
    /// </summary>
    public class SubscriptionCreateOrUpdateRequest
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
