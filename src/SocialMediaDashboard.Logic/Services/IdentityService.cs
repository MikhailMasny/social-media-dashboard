using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Common.DTO;
using SocialMediaDashboard.Common.Helpers;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IOptionsSnapshot<JwtSettings> _jwtSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public IdentityService(IOptionsSnapshot<JwtSettings> jwtSettings,
                               UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               IRepository<RefreshToken> refreshTokenRepository,
                               TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
            _tokenValidationParameters = tokenValidationParameters ?? throw new ArgumentNullException(nameof(tokenValidationParameters));
        }

        /// <inheritdoc/>
        public async Task<ConfirmationResult> RegistrationAsync(string email, string userName, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser != null)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { "The email you specified is already in the system." }
                };
            }

            var user = new IdentityUser
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

            await _userManager.AddToRoleAsync(user, "User");

            return new ConfirmationResult
            {
                IsSuccessful = true,
                Email = user.Email,
                Code = await _userManager.GenerateEmailConfirmationTokenAsync(user)
            };
        }

        /// <inheritdoc/>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(identityUser, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Email or password is incorrect." }
                };
            }

            var emailConfirmationResult = await EmailConfirmHandlerAsync(identityUser);

            if (!emailConfirmationResult.IsSuccessful)
            {
                return new AuthenticationResult
                {
                    Errors = emailConfirmationResult.Errors
                };
            }

            return await GenerateAuthenticationResultAsync(identityUser);
        }

        /// <inheritdoc/>
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid token." }
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value, CultureInfo.InvariantCulture);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This token hasn't expired yet." }
                };
            }

            var jti = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _refreshTokenRepository.GetEntityAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token does not exist" } 
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has expired" }
                };
            }

            if (storedRefreshToken.IsInvalid)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been invalidated" }
                };
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been used" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token does not match this JWT" }
                };
            }

            storedRefreshToken.IsUsed = true;
            _refreshTokenRepository.Update(storedRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var identityUser = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateAuthenticationResultAsync(identityUser);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> ConfirmEmailAsync(string email, string code)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, code);

            if (!identityResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Unexpected token issues.." }
                };
            }

            return await GenerateAuthenticationResultAsync(identityUser);
        }

        /// <inheritdoc />
        public async Task<ConfirmationResult> RestorePasswordAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var emailConfirmationResult = await EmailConfirmHandlerAsync(identityUser);

            if (!emailConfirmationResult.IsSuccessful)
            {
                return new ConfirmationResult
                {
                    Errors = emailConfirmationResult.Errors
                };
            }

            return new ConfirmationResult
            {
                IsSuccessful = true,
                Email = identityUser.Email,
                Code = await _userManager.GeneratePasswordResetTokenAsync(identityUser)
            };
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> ResetPasswordAsync(string email, string newPassword, string code)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var identityResult = await _userManager.ResetPasswordAsync(identityUser, code, newPassword);

            if (!identityResult.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "An unexpected error occurred while resetting the password.." }
                };
            }

            return await GenerateAuthenticationResultAsync(identityUser);
        }

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByEmailAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
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

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByIdAsync(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);

            if (identityUser == null)
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

        /// <inheritdoc/>
        public async Task<UserResult> GetUserByNameAsync(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if (identityUser == null)
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

        private async Task<ConfirmationResult> EmailConfirmHandlerAsync(IdentityUser identityUser)
        {
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(identityUser);

            if (!isConfirmed)
            {
                return new ConfirmationResult
                {
                    Errors = new[] { "You have not verified your email." }
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

        private async Task<AuthenticationResult> GenerateAuthenticationResultAsync(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim("id", identityUser.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(identityUser);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null)
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
                UserId = identityUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
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
