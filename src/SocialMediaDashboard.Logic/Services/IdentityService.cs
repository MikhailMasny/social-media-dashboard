using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Constants;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Helpers;
using SocialMediaDashboard.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IIdentityService"/>
    public class IdentityService : IIdentityService
    {
        private readonly IOptionsSnapshot<JwtSettings> _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IProfileService _profileService;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public IdentityService(IOptionsSnapshot<JwtSettings> jwtSettings,
                               UserManager<User> userManager,
                               RoleManager<IdentityRole> roleManager,
                               IRepository<RefreshToken> refreshTokenRepository,
                               TokenValidationParameters tokenValidationParameters,
                               IProfileService profileService)
        {
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
            _tokenValidationParameters = tokenValidationParameters ?? throw new ArgumentNullException(nameof(tokenValidationParameters));
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        }

        public async Task<ConfirmationResult> SignUpAsync(string email, string password, string name)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser != null)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { IdentityResource.EmailAlreadyExist }
                };
            }

            var userName = Guid.NewGuid()
                .ToString()
                .Replace("-", "", StringComparison.InvariantCulture);

            var user = new User
            {
                Email = email,
                UserName = userName
            };

            var createdUser = await _userManager.CreateAsync(user, password);

            if (!createdUser.Succeeded)
            {
                return new ConfirmationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            await _profileService.CreateAsync(user.Id, name);
            await _userManager.AddToRoleAsync(user, AppRole.User);

            return new ConfirmationResult
            {
                IsSuccessful = true,
                Email = user.Email,
                Code = await _userManager.GenerateEmailConfirmationTokenAsync(user)
            };
        }

        public async Task<AuthenticationResult> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.UserNotExist }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.IncorrectData }
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

            return await GenerateAuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.UserNotExist }
                };
            }

            var identityResult = await _userManager.ConfirmEmailAsync(user, code);

            if (!identityResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.TokenException }
                };
            }

            return await GenerateAuthenticationResultAsync(user);
        }

        public async Task<UserResult> GetUserByEmailAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser is null)
            {
                return null;
            }

            return new UserResult
            {
                Id = identityUser.Id,
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber
            };
        }

        public async Task<UserResult> GetUserByIdAsync(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);

            if (identityUser is null)
            {
                return null;
            }

            return new UserResult
            {
                Id = identityUser.Id,
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber
            };
        }

        public async Task<UserResult> GetUserByNameAsync(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if (identityUser is null)
            {
                return null;
            }

            return new UserResult
            {
                Id = identityUser.Id,
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber
            };
        }

        public async Task<ConfirmationResult> RestorePasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { IdentityResource.UserNotExist }
                };
            }

            var emailConfirmationResult = await EmailConfirmHandlerAsync(user);

            if (!emailConfirmationResult.IsSuccessful)
            {
                return new ConfirmationResult
                {
                    Errors = emailConfirmationResult.Errors
                };
            }

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new ConfirmationResult
            {
                IsSuccessful = true,
                Email = user.Email,
                Code = passwordResetToken
            };
        }

        public async Task<AuthenticationResult> ResetPasswordAsync(string email, string newPassword, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.UserNotExist }
                };
            }

            var identityResult = await _userManager.ResetPasswordAsync(user, code, newPassword);

            if (!identityResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.PasswordException }
                };
            }

            return await GenerateAuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.TokenInvalid }
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value, CultureInfo.InvariantCulture);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.TokenNotExpired }
                };
            }

            var jti = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _refreshTokenRepository.GetEntityAsync(x => x.Token == refreshToken);

            if (storedRefreshToken is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.RefreshTokenNotExist }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.RefreshTokenExpired }
                };
            }

            if (storedRefreshToken.IsInvalid)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.RefreshTokenInvalid }
                };
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.RefreshTokenUsed }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { IdentityResource.RefreshTokenNotMatch }
                };
            }

            storedRefreshToken.IsUsed = true;
            _refreshTokenRepository.Update(storedRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == CommonResource.Id).Value);
            return await GenerateAuthenticationResultAsync(user);
        }

        private async Task<ConfirmationResult> EmailConfirmHandlerAsync(User user)
        {
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!isConfirmed)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { IdentityResource.EmailNotVerified }
                };
            }

            return new ConfirmationResult
            {
                IsSuccessful = true
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidaSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsJwtWithValidaSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CommonResource.Id, user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role is null)
                {
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                    {
                        continue;
                    }

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.Value.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _refreshTokenRepository.CreateAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new AuthenticationResult
            {
                IsSuccessful = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
