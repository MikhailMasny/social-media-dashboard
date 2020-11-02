namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Authentication data transfet object.
    /// </summary>
    public class AuthenticationDto
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
