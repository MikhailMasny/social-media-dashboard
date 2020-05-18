namespace SocialMediaDashboard.Common.Interfaces
{
    /// <summary>
    /// Interface for implement User identity.
    /// </summary>
    public interface IHasUserIdentity
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }
    }
}
