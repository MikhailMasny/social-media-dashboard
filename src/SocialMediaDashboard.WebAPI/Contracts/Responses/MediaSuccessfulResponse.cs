namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Media successful response.
    /// </summary>
    public class MediaSuccessfulResponse
    {
        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }
    }
}
