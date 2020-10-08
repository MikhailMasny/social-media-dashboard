namespace SocialMediaDashboard.Web.Models
{
    /// <summary>
    /// Error model for validation filter.
    /// </summary>
    public class ValidationErrorModel
    {
        /// <summary>
        /// Field name.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }
    }
}
