using SocialMediaDashboard.Domain.Enums;

namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Social media account data transfet object.
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public AccountKind Type { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }
    }
}
