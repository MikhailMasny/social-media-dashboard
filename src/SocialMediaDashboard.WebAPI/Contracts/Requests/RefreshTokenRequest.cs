namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Refresh token request.
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
