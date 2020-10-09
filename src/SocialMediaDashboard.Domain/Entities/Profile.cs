namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// User profile entity.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation for User.
        /// </summary>
        public User User { get; set; }
    }
}
