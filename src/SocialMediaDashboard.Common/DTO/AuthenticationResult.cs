namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Authentication data transfet object.
    /// </summary>
    public class AuthenticationResult : BaseResult
    {
        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh JWT Token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
