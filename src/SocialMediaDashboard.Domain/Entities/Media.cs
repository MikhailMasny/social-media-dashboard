using Microsoft.AspNetCore.Identity;
using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Media model.
    /// </summary>
    public class Media : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        // TODO: IsDisplayed

        // UNDONE: maybe fix it?
        /// <summary>
        /// User.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        /// <summary>
        /// Navigation to Statistic.
        /// </summary>
        public ICollection<Statistic> Statistics { get; }
    }
}
