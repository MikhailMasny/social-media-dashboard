namespace SocialMediaDashboard.WebAPI.Contracts.Queries
{
    /// <summary>
    /// Email query.
    /// </summary>
    public class EmailQuery
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        public string Code { get; set; }
    }
}
