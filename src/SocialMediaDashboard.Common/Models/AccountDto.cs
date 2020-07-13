using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.Models
{
    /// <summary>
    /// Social media account data transfet object.
    /// </summary>
    public class AccountDto : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }
    }
}
