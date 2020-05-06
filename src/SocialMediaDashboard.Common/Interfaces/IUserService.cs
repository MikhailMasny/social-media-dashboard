using SocialMediaDashboard.Common.DTO;
using System.Collections.Generic;

namespace SocialMediaDashboard.Common.Interfaces
{
    public interface IUserService
    {
        UserDTO Authenticate(string username, string password);
        IEnumerable<UserDTO> GetAll();
    }
}
