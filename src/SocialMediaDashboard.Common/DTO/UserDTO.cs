using SocialMediaDashboard.Common.Interfaces;
using System.Text.Json.Serialization;

namespace SocialMediaDashboard.Common.DTO
{
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
