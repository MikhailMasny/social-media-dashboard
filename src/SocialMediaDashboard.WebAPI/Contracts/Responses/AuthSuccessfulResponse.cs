namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Authentication successful response.
    /// </summary>
    public class AuthSuccessfulResponse
    {
        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }
    }
}
