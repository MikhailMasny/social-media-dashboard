using SocialMediaDashboard.Application.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.Web.Contracts.Responses
{
    /// <summary>
    /// Social media account successful response.
    /// </summary>
    public class AccountSuccessfulResponse
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Accounts.
        /// </summary>
        public List<AccountDto> Accounts { get; } = new List<AccountDto>();
    }
}
