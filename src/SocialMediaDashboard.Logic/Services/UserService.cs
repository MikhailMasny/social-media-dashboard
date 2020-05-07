using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Common.DTO;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserDTO> _users = new List<UserDTO>
        {
            new UserDTO { Id = 1, Email = "Test@Test", Password = "test", Name = "User", IsAdmin = false }
        };

        private readonly ApplicationSettings _appSettings;
        private readonly IRepository<User> _userRepository;

        public UserService(IOptions<ApplicationSettings> appSettings,
                           IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <inheritdoc/>
        public async Task<UserDTO> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetEntity(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                // UNDONE: Response
                return null;
            }

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDTO.Token = tokenHandler.WriteToken(token);

            return userDTO;
        }

        /// <inheritdoc/>
        public IEnumerable<UserDTO> GetAll()
        {
            return _users;
        }
    }
}
