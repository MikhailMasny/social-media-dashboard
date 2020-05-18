using Microsoft.AspNetCore.Identity;
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

        public async Task<AuthenticationResult> CreateUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email // UNDONE: username
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            var token = GetToken(newUser.Id, newUser.Email);

            return new AuthenticationResult
            {
                IsSuccessful = true,
                Token = token
            };
        }

        private string GetToken(string id, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
