using SocialMediaDashboard.Common.DTO;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> CreateUserAsync(string email, string password);
    }
}
