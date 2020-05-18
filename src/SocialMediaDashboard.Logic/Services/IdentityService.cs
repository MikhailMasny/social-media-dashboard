using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Common.DTO;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IIdentityService"/>
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(IOptions<ApplicationSettings> appSettings,
                               UserManager<IdentityUser> userManager)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc/>
        public async Task<RegistrationResult> RegistrationAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return new RegistrationResult
                {
                    Errors = new[] { "The email you specified is already in the system." }
                };
            }

            var identityUser = new IdentityUser
            {
                Email = email,
                UserName = email // UNDONE: username
            };

            var createdUser = await _userManager.CreateAsync(identityUser, password);

            if (!createdUser.Succeeded)
            {
                return new RegistrationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return new RegistrationResult
            {
                IsSuccessful = true,
                UserId = identityUser.Id,
                Code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser)
            };
        }

        /// <inheritdoc/>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Email or password is incorrect." }
                };
            }

            var emailConfirmationResult = await EmailConfirmHandlerAsync(user);

            if (!emailConfirmationResult.IsSuccessful)
            {
                return new AuthenticationResult
                {
                    Errors = emailConfirmationResult.Errors
                };
            }

            return GenerateAuthenticationResult(user);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> ConfirmEmailAsync(string id, string code)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var identityResult = await _userManager.ConfirmEmailAsync(user, code);

            if (!identityResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Unexpected token issues.." }
                };
            }

            return GenerateAuthenticationResult(user);
        }

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserResult
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResult
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            return new UserResult
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        private async Task<RegistrationResult> EmailConfirmHandlerAsync(IdentityUser identityUser)
        {
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(identityUser);

            if (!isConfirmed)
            {
                return new RegistrationResult
                {
                    Errors = new[] { "You have not verified your email." }
                };
            }

            return new RegistrationResult
            {
                IsSuccessful = true
            };
        }

        private AuthenticationResult GenerateAuthenticationResult(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, identityUser.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                IsSuccessful = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
