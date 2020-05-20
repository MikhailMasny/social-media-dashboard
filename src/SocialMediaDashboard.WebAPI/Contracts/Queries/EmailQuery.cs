namespace SocialMediaDashboard.WebAPI.Contracts.Queries
{
    /// <summary>
    /// Email query.
    /// </summary>
    public class EmailQuery
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        public string Code { get; set; }
    }
}
