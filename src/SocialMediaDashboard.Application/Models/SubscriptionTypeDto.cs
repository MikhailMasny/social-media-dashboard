namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Subscription type data transfer object.
    /// </summary>
    public class SubscriptionTypeDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Platform identifier.
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// Observation identifier.
        /// </summary>
        public int ObservationId { get; set; }
    }
}
