namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Statistic data transfet object.
    /// </summary>
    public class SubscriptionDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Account name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Subscription type identifier.
        /// </summary>
        public int SubscriptionTypeId { get; set; }
    }
}
