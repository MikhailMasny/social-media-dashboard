using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Media data transfet object.
    /// </summary>
    public class MediaDto : IHasUserIdentity
    {
        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }
    }
}
