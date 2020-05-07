using SocialMediaDashboard.Common.Interfaces;
using System.Text.Json.Serialization;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// UserDTO model.
    /// </summary>
    public class UserDTO : IHasDbIdentity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// JWT Token.
        /// </summary>
        public string Token { get; set; }
    }
}
