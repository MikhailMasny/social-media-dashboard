using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Common.DTO;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public async Task<ResponseDTO> Registration(string email, string password, string name)
        {
            var existingUser = await _userRepository.GetEntity(x => x.Email == email);

            if (existingUser != null)
            {
                return new ResponseDTO
                {
                    Result = false,
                    Message = "The email you specified is already in the system."
                };
            }

            var user = new User
            {
                Email = email,
                Password = ConvertPassword(password),
                Name = name,
                Role = Roles.User.ToString()
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Avatar = user.Avatar,
                Name = user.Name,
                Role = user.Role
            };

            return new ResponseDTO
            {
                Result = true,
                Message = "User successfully registered.",
                User = userDTO,
                Token = GetToken(user.Id, user.Email, user.Role)
            };
        }

        /// <inheritdoc/>
        public async Task<ResponseDTO> Authenticate(string email, string password)
        {
            var convertedPassword = ConvertPassword(password);
            var user = await _userRepository.GetEntity(x => x.Email == email && x.Password == convertedPassword);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Result = false,
                    Message = "Email or password is incorrect."
                };
            }

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Avatar = user.Avatar,
                Name = user.Name,
                Role = user.Role
            };

            return new ResponseDTO
            {
                Result = true,
                Message = "User successfully logged in.",
                User = userDTO,
                Token = GetToken(user.Id, user.Email, user.Role)
            };
        }

        /// <inheritdoc/>
        public async Task<ResponseDTO> GetProfile(int userId)
        {
            var user = await _userRepository.GetEntity(x => x.Id == userId);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Result = false,
                    Message = "User with the specified email address was not found."
                };
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Avatar = user.Avatar,
                Name = user.Name,
                Role = user.Role
            };

            return new ResponseDTO
            {
                Result = true,
                Message = "User data updated successfully.",
                User = userDTO
            };
        }

        /// <inheritdoc/>
        public async Task<ResponseDTO> UpdateProfile(TokenDTO tokenData, string name, string avatar)
        {
            // UNDONE: to response model
            var user = await _userRepository.GetEntity(x => x.Id == tokenData.Id);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Result = false,
                    Message = "User with the specified email address was not found."
                };
            }

            user.Name = name;
            user.Avatar = avatar;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            // UNDONE: Automapper
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Avatar = user.Avatar,
                Name = user.Name,
                Role = user.Role
            };

            return new ResponseDTO
            {
                Result = true,
                Message = "User data updated successfully.",
                User = userDTO
            };
        }

        /// <inheritdoc/>
        public TokenDTO GetUserData(ClaimsPrincipal claimsPrincipal)
        {
            return new TokenDTO
            {
                Id = int.Parse(claimsPrincipal.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value),
                Email = claimsPrincipal.Claims.Where(a => a.Type == ClaimTypes.Email).FirstOrDefault().Value,
                Role = claimsPrincipal.Claims.Where(a => a.Type == ClaimTypes.Role).FirstOrDefault().Value
            };
        }

        private string GetToken(int id, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string ConvertPassword(string password)
        {
            var sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
