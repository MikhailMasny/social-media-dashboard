using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaDashboard.Application.Exceptions;
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

        public async Task<string> SignUpAsync(string email, string password, string name)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser != null)
            {
                throw new ConflictException(IdentityResource.EmailAlreadyExist);
            }

            var user = new User
            {
                Email = email,
                UserName = Guid.NewGuid()
                    .ToString()
                    .Replace("-", "", StringComparison.InvariantCulture),
            };

            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded)
            {
                throw new ConflictException(string.Join(" ", createdUser.Errors.Select(x => x.Description)));
            }

            await _profileService.CreateAsync(user.Id, name);
            await _userManager.AddToRoleAsync(user, AppRole.User);

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<AuthenticationDto> SignInAsync(string email, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser is null)
            {
                throw new NotFoundException(IdentityResource.UserNotExist);
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(identityUser, password);
            if (!isValidPassword)
            {
                throw new AppException(IdentityResource.IncorrectData);
            }

            await EmailConfirmHandlerAsync(identityUser);

            return await GenerateAuthenticationResultAsync(identityUser);
        }

        public async Task<AuthenticationDto> ConfirmEmailAsync(string email, string code)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser is null)
            {
                throw new NotFoundException(IdentityResource.UserNotExist);
            }

            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, code);
            if (!identityResult.Succeeded)
            {
                throw new AppException(IdentityResource.TokenException);
            }

            return await GenerateAuthenticationResultAsync(identityUser);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser is null)
            {
                throw new NotFoundException(IdentityResource.UserNotExist);
            }

            return new UserDto
            {
                Id = identityUser.Id,
                Email = identityUser.Email,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);
            return identityUser is null
                ? throw new NotFoundException(IdentityResource.UserNotExist)
                : new UserDto
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber
                };
        }

        public async Task<UserDto> GetUserByNameAsync(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);
            return identityUser is null
                ? throw new NotFoundException(IdentityResource.UserNotExist)
                : new UserDto
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                };
        }

        public async Task<ConfirmationDto> RestorePasswordAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser is null)
            {
                throw new NotFoundException(IdentityResource.UserNotExist);
            }

            await EmailConfirmHandlerAsync(identityUser);

            return new ConfirmationDto
            {
                Data = (await _profileService.GetByUserIdAsync(identityUser.Id)).Name,
                Code = await _userManager.GeneratePasswordResetTokenAsync(identityUser),
            };
        }

        public async Task<(string name, AuthenticationDto authenticationDto)> ResetPasswordAsync(string email, string newPassword, string code)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser is null)
            {
                throw new NotFoundException(IdentityResource.UserNotExist);
            }

            var identityResult = await _userManager.ResetPasswordAsync(identityUser, code, newPassword);
            if (!identityResult.Succeeded)
            {
                throw new AppException(IdentityResource.PasswordException);
            }

            return ((await _profileService.GetByUserIdAsync(identityUser.Id)).Name,
                await GenerateAuthenticationResultAsync(identityUser));
        }

        public async Task<AuthenticationDto> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            var expiryDateUnix = long.Parse(
                validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value,
                CultureInfo.InvariantCulture);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(long.Parse(
                    validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value,
                    CultureInfo.InvariantCulture));

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                throw new AppException(IdentityResource.TokenNotExpired);
            }

            var jti = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _refreshTokenRepository.GetEntityAsync(x => x.Token == refreshToken);
            if (storedRefreshToken is null)
            {
                throw new NotFoundException(IdentityResource.RefreshTokenNotExist);
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                throw new AppException(IdentityResource.RefreshTokenExpired);
            }

            if (storedRefreshToken.IsInvalid)
            {
                throw new AppException(IdentityResource.RefreshTokenInvalid);
            }

            if (storedRefreshToken.IsUsed)
            {
                throw new AppException(IdentityResource.RefreshTokenUsed);
            }

            if (storedRefreshToken.JwtId != jti)
            {
                throw new AppException(IdentityResource.RefreshTokenNotMatch);
            }

            storedRefreshToken.IsUsed = true;
            _refreshTokenRepository.Update(storedRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x =>
                x.Type == CommonResource.Id).Value);

            return await GenerateAuthenticationResultAsync(user);
        }

        private async Task EmailConfirmHandlerAsync(User user)
        {
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
            {
                throw new AppException(IdentityResource.EmailNotVerified);
            }
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidaSecurityAlgorithm(validatedToken))
                {
                    throw new AppException(IdentityResource.TokenInvalid);
                }

                return principal;
            }
            catch (Exception)
            {
                throw new AppException(IdentityResource.TokenInvalid);
            }
        }

        private bool IsJwtWithValidaSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationDto> GenerateAuthenticationResultAsync(User user)
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

            var token =
                tokenHandler.CreateToken(
                    new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.Add(_jwtSettings.Value.TokenLifetime),
                        SigningCredentials =
                            new SigningCredentials(
                                new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256Signature),
                    });

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

            return new AuthenticationDto
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
