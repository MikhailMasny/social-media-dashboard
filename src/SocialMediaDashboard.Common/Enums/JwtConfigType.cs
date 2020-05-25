namespace SocialMediaDashboard.Common.Enums
{
    /// <summary>
    /// Application JWT configuration.
    /// </summary>
    public enum JwtConfigType
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Secret.
        /// </summary>
        Secret = 1,

        /// <summary>
        /// Token lifetime.
        /// </summary>
        TokenLifetime = 2
    }
}
