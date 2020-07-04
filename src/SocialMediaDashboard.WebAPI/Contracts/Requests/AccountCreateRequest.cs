using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Social media account create request.
    /// </summary>
    public class AccountCreateRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account type.
        /// </summary>
        public AccountType AccountType { get; set; }
    }
}
