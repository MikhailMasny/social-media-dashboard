using SocialMediaDashboard.Domain.Enums;

namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Social media account create or update request.
    /// </summary>
    public class AccountCreateOrUpdateRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account type.
        /// </summary>
        public AccountKind AccountType { get; set; }
    }
}
