namespace SocialMediaDashboard.WebAPI.Models
{
    /// <summary>
    /// Error model for validation filter.
    /// </summary>
    public class ValidationErrorModel
    {
        /// <summary>
        /// Field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }
    }
}
