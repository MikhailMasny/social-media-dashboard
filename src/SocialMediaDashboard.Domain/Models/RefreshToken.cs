using Microsoft.AspNetCore.Identity;
using SocialMediaDashboard.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// Refresh Token.
    /// </summary>
    public class RefreshToken : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// JWT Identifier.
        /// </summary>
        public string JwtId { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Expiry date.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Use indicator.
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Invalid indicator.
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        // UNDONE: maybe fix it?
        /// <summary>
        /// User.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
