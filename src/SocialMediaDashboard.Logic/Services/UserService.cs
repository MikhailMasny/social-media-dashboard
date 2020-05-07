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
        private readonly ApplicationSettings _appSettings;
        private readonly IRepository<User> _userRepository;

        public UserService(IOptions<ApplicationSettings> appSettings,
                           IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <inheritdoc/>
        public async Task<(bool result, string message, UserDTO user)> Registration(string email, string password, string name)
        {
            var existingUser = await _userRepository.GetEntity(x => x.Email == email);

            if (existingUser != null)
            {
                // UNDONE: Response
                return (false, "The email you specified is already in the system.", null);
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Avatar = "Helloworld",
                Name = name,
                IsAdmin = false
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Avatar = user.Avatar,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            };

            userDTO.Token = GetToken(user.Id);

            return (true, "User successfully registered.", userDTO);
        }

        /// <inheritdoc/>
        public async Task<(bool result, string message, UserDTO user)> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetEntity(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                // UNDONE: Response
                return (false, "Email or password is incorrect.", null);
            }

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Avatar = user.Avatar,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            };

            userDTO.Token = GetToken(user.Id);

            return (true, "User successfully logged in.", userDTO);
        }

        private string GetToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
