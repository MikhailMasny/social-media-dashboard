namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Authentication result.
    /// </summary>
    public class AuthenticationResult : ResultBase
    {
        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }
    }
}
