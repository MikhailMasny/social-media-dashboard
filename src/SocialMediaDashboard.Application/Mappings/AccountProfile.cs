using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// Профиль AutoMapper для Account.
    /// </summary>
    public class AccountProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
